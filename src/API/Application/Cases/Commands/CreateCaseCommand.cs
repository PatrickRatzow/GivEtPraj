using System.Net;
using System.Reflection;
using Commentor.GivEtPraj.Application.Common.Security;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

[ReCaptcha(AllowQueue = true)]
public class CreateCaseCommand : IRequest<OneOf<Unit, InvalidCategory, InvalidSubCategories>>
{
    public CreateCaseCommand(List<CaseCreationDto> cases)
    {
        Cases = cases;
    }

    public List<CaseCreationDto> Cases { get; set; }
}

public class
    CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, OneOf<Unit, InvalidCategory, InvalidSubCategories>>
{
    private readonly IAppDbContext _db;
    private readonly IImageStorage _imageStorage;
    private readonly IDeviceService _deviceService;

    public CreateCaseCommandHandler(IAppDbContext db, IImageStorage imageStorage, IDeviceService deviceService)
    {
        _db = db;
        _imageStorage = imageStorage;
        _deviceService = deviceService;
    }

    public async Task<OneOf<Unit, InvalidCategory, InvalidSubCategories>>
        Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var categories = await _db.Categories
            .Include(cat => cat.SubCategories)
            .Where(cat => request.Cases.Select(@case => @case.CategoryId).Contains(cat.Id))
            .ToDictionaryAsync(cat => cat.Id, cat => cat, cancellationToken);

        if (!ValidateCategory(request, categories)) return new InvalidCategory();
        if (!ValidateSubCategories(request, categories)) return new InvalidSubCategories();

        foreach (var @case in request.Cases)
        {
            var images = await CreateImages(@case);

            var newCase = @case switch
            {
                { SubCategoryIds: not null } =>
                    CreateCase(@case, categories.Values.First(c => c.Id == @case.CategoryId).SubCategories.ToList(), images),
                { SubCategoryIds: null } => 
                    CreateMiscellaneousCase(@case, images),
                _ => throw new ArgumentOutOfRangeException()
            };

            _db.Cases.Add(newCase);
        }

        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
    private static bool ValidateCategory(CreateCaseCommand request, Dictionary<int, Category> categories)
    {
        var distinctRequestCategoryCount = request.Cases.DistinctBy(c => c.CategoryId).Count();
        if (categories.Count != distinctRequestCategoryCount) return false;
        
        foreach (var @case in request.Cases)
        {
            var isMiscellaneous = categories.GetValueOrDefault(@case.CategoryId)?.Miscellaneous == true;
            if (!isMiscellaneous) continue;

            var subCategoriesCount = @case.SubCategoryIds?.Length;
            if (subCategoriesCount > 0) continue;

            return false;
        }

        return true;
    }

    private static bool ValidateSubCategories(CreateCaseCommand request, Dictionary<int, Category> categories)
    {
        var requestHasAnySubCategoriesNotFound = request.Cases
            .Any(c => {
                var isMiscellaneous = categories.GetValueOrDefault(c.CategoryId)?.Miscellaneous == true;
                if (isMiscellaneous) return false;
                if (c.SubCategoryIds?.Length == 0) return false;
                
                // Check that the category in database has all the sub categories in the request
                return categories.Values
                    .Select(cat => cat.SubCategories.Select(sub => sub.Id))
                    .Contains(c.SubCategoryIds);
            });
        
        return !requestHasAnySubCategoriesNotFound;
    }
    private async Task<List<CaseImage>> CreateImages(CaseCreationDto @case)
    {
        var images = new List<CaseImage>();
        var list = new List<(Stream Image, Guid Id)>();
        foreach (var image in @case.Images)
        {
            var streamImage = new MemoryStream(Convert.FromBase64String(image));
            var guid = Guid.NewGuid();
            list.Add((streamImage, guid));
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

    private BaseCase CreateCase(CaseCreationDto @case, List<SubCategory> subCategories, List<CaseImage> images)
    {
        return new Case
        {
            Comment = @case.Comment!,
            SubCategories = subCategories,
            Images = images,
            CategoryId = @case.CategoryId,
            GeographicLocation = GeographicLocation.From(@case.Latitude, @case.Longitude),
            DeviceId = _deviceService.DeviceIdentifier
        };
    }

    private BaseCase CreateMiscellaneousCase(CaseCreationDto @case, List<CaseImage> images)
    {
        return new MiscellaneousCase
        {
            Description = @case.Description!,
            Images = images,
            CategoryId = @case.CategoryId,
            GeographicLocation = GeographicLocation.From(@case.Latitude, @case.Longitude),
            DeviceId = _deviceService.DeviceIdentifier
        };
    }
}

public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
{
    public CreateCaseCommandValidator()
    {
        RuleForEach(c => c.Cases).ChildRules(@case => {
            @case.RuleFor(x => x.Longitude)
                .LessThanOrEqualTo(180)
                .GreaterThanOrEqualTo(-180);

            @case.RuleFor(x => x.Latitude)
                .LessThanOrEqualTo(90)
                .GreaterThanOrEqualTo(-90);

            @case.RuleFor(x => x.CategoryId)
                .NotEmpty();

            @case.RuleForEach(x => x.Images)
                .NotEmpty();

            @case.When(x => x.SubCategoryIds != null, () => {
                @case.RuleFor(x => x.SubCategoryIds!.Length)
                    .NotNull()
                    .LessThanOrEqualTo(3);

                @case.RuleFor(x => x.Comment)
                    .MaximumLength(4096);
            }).Otherwise(() => {
                @case.RuleFor(x => x.Description)
                    .NotEmpty()
                    .MaximumLength(4096);
            });
        });
    }

    private bool ValidateIP(string ipString)
    {
        return IPAddress.TryParse(ipString, out _);
    }
}