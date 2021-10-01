using System.IO;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

public record CreateCaseCommand(
    string Title,
    string Description,
    IList<string> Images,
    string Category
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
            Category = category
        };
            
        _db.Cases.Add(newCase);
        await _db.SaveChangesAsync(cancellationToken);

        var summaryDto = _mapper.Map<Case, CaseSummaryDto>(newCase);
        return summaryDto;
    }

    private async Task<List<Picture>> CreateImages(CreateCaseCommand request)
    {
        var images = request.Images
            .Select(i => new Picture
            {
                Id = Guid.NewGuid()
            })
            .ToList();

        await UploadImages(images);
        
        return images;
    }

    private async ValueTask UploadImages(IReadOnlyList<Picture> images)
    {
        if (!images.Any()) return;

        var disposables = new List<IAsyncDisposable>();
        await Task.WhenAll(images.Select((cp, index) =>
        { 
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            disposables.Add(stream);
            disposables.Add(writer);
            
            writer.Write(images[index]);
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

        RuleFor(x => x.Category)
            .NotEmpty();

        RuleForEach(x => x.Images)
            .NotEmpty();
    }
}