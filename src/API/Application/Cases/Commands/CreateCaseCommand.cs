using System.Net;
using Commentor.GivEtPraj.Application.Common.Security;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

[ReCaptcha]
public class CreateCaseCommand : IRequest<OneOf<int, InvalidCategory, InvalidSubCategories>>
{
    public CreateCaseCommand(Guid deviceId, IPAddress ipAddress, List<CaseCreationDto> cases)
    {
        DeviceId = deviceId;
        IpAddress = ipAddress;
        Cases = cases;
    }

    public IPAddress IpAddress { get; set; }
    public Guid DeviceId { get; set; }
    public List<CaseCreationDto> Cases { get; set; }
}

public class
    CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, OneOf<int, InvalidCategory, InvalidSubCategories>>
{
    private readonly IAppDbContext _db;
    private readonly IImageStorage _imageStorage;

    public CreateCaseCommandHandler(IAppDbContext db, IImageStorage imageStorage)
    {
        _db = db;
        _imageStorage = imageStorage;
    }

    public async Task<OneOf<int, InvalidCategory, InvalidSubCategories>>
        Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var categoriesValid = await _db.Categories.AllAsync(cat => request.Cases.Any(@case => @case.CategoryId == cat.Id));
        if (!categoriesValid) return new InvalidCategory();
        
        request.Cases.Select(@case =>
        {
            @case switch
            {
                { Description: null} => CreateCase(@case, request.DeviceId, request.IpAddress, cancellationToken)
            }
        })

        var images = await CreateImages(request);

        BaseCase newCase;
        if (request.Description is null)
        {
            var subCategories = await _db.SubCategories
                .Where(sc => sc.Category.Id == categoriesValid.Id && request.SubCategories!.Contains(sc.Id))
                .ToListAsync(cancellationToken);

            if (subCategories.Count != request.SubCategories!.Length)
                return new InvalidSubCategories(request.SubCategories);

            newCase = new Case
            {
                Comment = request.Comment!,
                SubCategories = subCategories,
                Images = images,
                Category = categoriesValid,
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
                Category = categoriesValid,
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

    private async Task<Case> CreateCase(Case @case, Guid deviceId, IPAddress ipAddress, CancellationToken cancellationToken)
    {
        List<SubCategory> subCategories = await _db.SubCategories.Where(subCat => @case.SubCategories.Any(sub => subCat.Id == sub.Id)).ToListAsync(cancellationToken);
        Case newCase = new Case
        {
            Comment =  @case.Comment!,
            SubCategories = subCategories,
            Images = images,
            Category = categoriesValid,
            GeographicLocation = GeographicLocation.From(request.Latitude, request.Longitude),
            Priority = request.Priority,
            IpAddress = request.IpAddress,
            DeviceId = request.DeviceId
        };
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