using System.Net;
using Commentor.GivEtPraj.Application.Common.Security;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

[ReCaptcha]
public class CreateCaseCommand : IRequest<OneOf<int, InvalidCategory, InvalidSubCategories>>
{
    public CreateCaseCommand(Guid deviceId, List<Stream> images, string category, double longitude,
        double latitude, Priority priority, IPAddress ipAddress, string description = "", string comment = "",
        string[]? subCategories = null)
    {
        DeviceId = deviceId;
        Comment = comment;
        Description = description;
        Images = images;
        Category = category;
        SubCategories = subCategories;
        Longitude = longitude;
        Latitude = latitude;
        Priority = priority;
        IpAddress = ipAddress;
    }

    public string? Comment { get; }
    public string? Description { get; }
    public List<Stream> Images { get; set; }
    public string Category { get; set; }
    public string[]? SubCategories { get; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public Priority Priority { get; set; }
    public IPAddress IpAddress { get; set; }
    public Guid DeviceId { get; set; }
}

public class
    CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, OneOf<int, InvalidCategory, InvalidSubCategories>>
{
    private readonly IAppDbContext _db;
    private readonly IImageStorage _imageStorage;
    private readonly IMapper _mapper;

    public CreateCaseCommandHandler(IAppDbContext db, IMapper mapper, IImageStorage imageStorage)
    {
        _db = db;
        _mapper = mapper;
        _imageStorage = imageStorage;
    }

    public async Task<OneOf<int, InvalidCategory, InvalidSubCategories>>
        Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var category = await _db.Categories
            .FirstOrDefaultAsync(c => request.Category == c.Name.English, cancellationToken);
        if (category is null) return new InvalidCategory(request.Category);

        var images = await CreateImages(request);

        BaseCase newCase;
        if (request.Description is null)
        {
            var subCategories = await _db.SubCategories
                .Where(sc => sc.Category.Id == category.Id && request.SubCategories!.Contains(sc.Name.English))
                .ToListAsync(cancellationToken);

            if (subCategories.Count != request.SubCategories!.Length)
                return new InvalidSubCategories(request.SubCategories);

            newCase = new Case
            {
                Comment = request.Comment!,
                SubCategories = subCategories,
                Images = images,
                Category = category,
                GeographicLocation = GeographicLocation.From(request.Latitude, request.Longitude),
                Priority = request.Priority,
                IpAddress = request.IpAddress,
                DeviceId = request.DeviceId
            };
        }
        else
        {
            newCase = new MiscellaneousCase
            {
                Description = request.Description!,
                Images = images,
                Category = category,
                GeographicLocation = GeographicLocation.From(request.Latitude, request.Longitude),
                Priority = request.Priority,
                IpAddress = request.IpAddress,
                DeviceId = request.DeviceId
            };
        }

        _db.Cases.Add(newCase);
        await _db.SaveChangesAsync(cancellationToken);

        return newCase.Id;
    }

    private async Task<List<CaseImage>> CreateImages(CreateCaseCommand request)
    {
        var images = new List<CaseImage>();
        var list = new List<(Stream Image, Guid Id)>();
        foreach (var image in request.Images)
        {
            var guid = Guid.NewGuid();
            list.Add((image, guid));
            images.Add(new()
            {
                Id = guid
            });
        }

        await UploadImages(list);

        return images;
    }

    private async ValueTask UploadImages(IReadOnlyCollection<(Stream Image, Guid Id)> images)
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

        RuleFor(x => x.Priority)
            .IsInEnum();

        RuleFor(x => x.IpAddress)
            .NotNull()
            .Must(x => ValidateIP(x.ToString()));

        RuleFor(x => x.DeviceId)
            .NotNull();

        When(x => x.SubCategories != null, () =>
        {
            RuleFor(x => x.SubCategories!.Length)
                .NotNull()
                .LessThanOrEqualTo(3);

            RuleFor(x => x.Comment)
                .MaximumLength(4096);
        }).Otherwise(() =>
        {
            RuleFor(x => x.Description)
                .MaximumLength(4096);
        });
    }

    private bool ValidateIP(string ipString)
    {
        return IPAddress.TryParse(ipString, out _);
    }
}