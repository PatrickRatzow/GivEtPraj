using System.Net;
using Commentor.GivEtPraj.Application.Common.Security;
using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

public class CreateCaseCommand : IRequest<OneOf<Unit, InvalidCategory, InvalidSubCategories>>
{
    public CreateCaseCommand()
    {
    }
    
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
    CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, OneOf<Unit, InvalidCategory, InvalidSubCategories>>
{
    private readonly IAppDbContext _db;
    private readonly IImageStorage _imageStorage;

    public CreateCaseCommandHandler(IAppDbContext db, IImageStorage imageStorage)
    {
        _db = db;
        _imageStorage = imageStorage;
    }

    public async Task<OneOf<Unit, InvalidCategory, InvalidSubCategories>>
        Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var categories = await _db.Categories
            .Include(cat => cat.SubCategories)
            .Where(cat => request.Cases.Select(@case => @case.CategoryId).Contains(cat.Id))
            .ToDictionaryAsync(cat => cat.Id, cat => cat, cancellationToken);
        var distinctRequestCategoryCount = request.Cases.DistinctBy(c => c.CategoryId).Count();
        if (categories.Count != distinctRequestCategoryCount) return new InvalidCategory();
        var requestHasAnySubCategoriesNotFound = request.Cases
            .Where(c => categories.GetValueOrDefault(c.CategoryId)?.Miscellaneous == true)
            .Any(c => 
                categories.GetValueOrDefault(c.CategoryId)?.Miscellaneous == true 
                || categories.Values
                    .Select(cat => cat.SubCategories.Select(sub => sub.Id))
                    .Contains(c.SubCategoryIds)
            );
        if (requestHasAnySubCategoriesNotFound) return new InvalidSubCategories();
        
        foreach (var @case in request.Cases)
        {
            var images = await CreateImages(@case);

            var newCase = @case switch
            {
                { Description: null, SubCategoryIds: not null, Comment: not null } =>
                    CreateCase(@case, request, categories.Values.First(c => c.Id == @case.CategoryId).SubCategories.ToList(), images),
                { Description: not null, SubCategoryIds: null, Comment: null } => 
                    CreateMiscellaneousCase(@case, request, images),
                _ => throw new ArgumentOutOfRangeException()
            };

            _db.Cases.Add(newCase);
        }

        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
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

    private static BaseCase CreateCase(CaseCreationDto @case, CreateCaseCommand request,
        List<SubCategory> subCategories, List<CaseImage> images)
    {
        return new Case
        {
            Comment = @case.Comment!,
            SubCategories = subCategories,
            Images = images,
            CategoryId = @case.CategoryId,
            GeographicLocation = GeographicLocation.From(@case.Latitude, @case.Longitude),
            Priority = @case.Priority,
            IpAddress = request.IpAddress,
            DeviceId = request.DeviceId
        };
    }

    private static BaseCase CreateMiscellaneousCase(CaseCreationDto @case, CreateCaseCommand request,
        List<CaseImage> images)
    {
        return new MiscellaneousCase
        {
            Description = @case.Description!,
            Images = images,
            CategoryId = @case.CategoryId,
            GeographicLocation = GeographicLocation.From(@case.Latitude, @case.Longitude),
            Priority = @case.Priority,
            IpAddress = request.IpAddress,
            DeviceId = request.DeviceId
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

            @case.RuleFor(x => x.Priority)
                .IsInEnum();
            
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
        

        RuleFor(x => x.IpAddress)
            .NotNull()
            .Must(x => ValidateIP(x.ToString()));

        RuleFor(x => x.DeviceId)
            .NotNull();
    }

    private bool ValidateIP(string ipString)
    {
        return IPAddress.TryParse(ipString, out _);
    }
}