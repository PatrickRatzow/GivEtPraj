using System.IO;
using System.Threading;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

public record CreateCaseCommand(string Title, string Description, IList<string> Images) : IRequest<CaseSummaryDto>;

public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, CaseSummaryDto>
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

    public async Task<CaseSummaryDto> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var images = await CreateImages(request);

        var newCase = new Case
        {
            Title = request.Title,
            Description = request.Description,
            Pictures = images
        };
            
        _db.Cases.Add(newCase);
        await _db.SaveChangesAsync(cancellationToken);

        var summaryDto = _mapper.Map<Case, CaseSummaryDto>(newCase);
        return summaryDto;
    }

    private async Task<List<CasePicture>> CreateImages(CreateCaseCommand request)
    {
        var images = request.Images
            .Select(i => new CasePicture
        {
            Id = Guid.NewGuid()
        }).ToList();

        await UploadImages(images);
        
        return images;
    }

    private async ValueTask UploadImages(IReadOnlyList<CasePicture> images)
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
    }
}