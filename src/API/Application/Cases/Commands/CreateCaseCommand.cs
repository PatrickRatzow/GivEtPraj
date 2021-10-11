using Commentor.GivEtPraj.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using static Commentor.GivEtPraj.Application.Services.CompressionService;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

public record CreateCaseCommand(
    string Title,
    string Description,
    IList<string> Images,
    string Category,
    double Longitude,
    double Latitude
) : IRequest<OneOf<CaseSummaryDto, InvalidCategory>>;

public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, OneOf<CaseSummaryDto, InvalidCategory>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IImageStorage _imageStorage;

    public CreateCaseCommandHandler(IAppDbContext db, IMapper mapper, IImageStorage imageStorage)
    {
        _db = db;
        _mapper = mapper;
        _imageStorage = imageStorage;
    }

    public async Task<OneOf<CaseSummaryDto, InvalidCategory>>
        Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var category = await _db.Categories
            .FirstOrDefaultAsync(c => request.Category == c.Name, cancellationToken);
        if (category is null) return new InvalidCategory(request.Category);

        var images = await CreateImages(request);

        var newCase = new Case
        {
            
            Description = request.Description,
            Pictures = images,
            Category = category,
            Coords = GeographicLocation.From(request.Latitude, request.Longitude)
        };

        _db.Cases.Add(newCase);
        await _db.SaveChangesAsync(cancellationToken);

        var summaryDto = _mapper.Map<Case, CaseSummaryDto>(newCase);
        return summaryDto;
    }

    private async Task<List<Picture>> CreateImages(CreateCaseCommand request)
    {
        var pictures = new List<Picture>();
        var list = new List<(string Image, Guid Id)>();
        foreach (var image in request.Images)
        {
            var guid = Guid.NewGuid();
            list.Add((image, guid));
            pictures.Add(new Picture { Id = guid });
        }


        var compressedList = new List<(string Image, Guid Id)>();
        foreach (var item in list)
        {
            MemoryStream img = VaryQualityLevel(Base64ToImage(item.Image), 30);

            compressedList.Add((Convert.ToBase64String(img.ToArray()), item.Id));

        }

        await UploadImages(compressedList);

        var xdPictures = new List<Picture>();
        foreach (var image in request.Images)
        {
            var guid = Guid.NewGuid();
            list.Add((image, guid));
            xdPictures.Add(new Picture { Id = guid });
        }

        return pictures.Concat(xdPictures).ToList();
    }

    private async ValueTask UploadImages(IReadOnlyList<(string Image, Guid Id)> images)
    {
        if (!images.Any()) return;

        var disposables = new List<IAsyncDisposable>();
        await Task.WhenAll(images.Select((cp, index) =>
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            disposables.Add(stream);
            disposables.Add(writer);

            writer.Write(cp.Image);
            writer.Flush();

            stream.Position = 0;

            return _imageStorage.UploadImage($"{cp.Id}.jpg", stream);
        }));

        foreach (var disposable in disposables)
        {
            await disposable.DisposeAsync();
        }
    }
}


public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
{
    public CreateCaseCommandValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(4)
            .MaximumLength(64);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(4096);

        RuleFor(x => x.Longitude)
            .LessThanOrEqualTo(180)
            .GreaterThanOrEqualTo(-180);

        RuleFor(x => x.Latitude)
            .LessThanOrEqualTo(90)
            .GreaterThanOrEqualTo(-90);

        RuleFor(x => x.Category)
            .NotEmpty();

        RuleForEach(x => x.Images)
            .NotEmpty();
    }
}
