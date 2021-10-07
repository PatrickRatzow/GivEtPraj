using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

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
            Title = request.Title,
            Description = request.Description,
            Pictures = images,
            Category = category,
            GeographicLocation = GeographicLocation.From(request.Latitude, request.Longitude)
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
            pictures.Add(new()
            {
                Id = guid
            });
        }

        await UploadImages(list);

        return pictures;
    }

    private async ValueTask UploadImages(IReadOnlyCollection<(string Image, Guid Id)> images)
    {
        if (images.Count == 0) return;

        var imageUploads = images.Select(img => _imageStorage.UploadImage($"{img.Id}.jpg", img.Image));

        await Task.WhenAll(imageUploads);
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