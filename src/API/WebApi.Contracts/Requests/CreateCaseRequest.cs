using FluentValidation;

namespace Commentor.GivEtPraj.WebApi.Contracts.Requests;

public class CreateCaseRequest
{

    public CreateCaseRequest()
    {
    }

    public CreateCaseRequest(string title, string description, List<string> images, string category, double longitude,
        double latitude)
    {
        Title = title;
        Description = description;
        Images = images;
        Category = category;
        Longitude = longitude;
        Latitude = latitude;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public string Category { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}

public class CreateCaseRequestValidator : AbstractValidator<CreateCaseRequest>
{
    public CreateCaseRequestValidator()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title skal være udfyldt")
            .MinimumLength(4).WithMessage("Titlen må ikke være mindre end 4 bogstaver")
            .MaximumLength(64).WithMessage("Titlen må ikke være mere end 64 bogstaver lang");

        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Beskrivelse skal være udfyldt")
            .MinimumLength(4).WithMessage("Beskrivelsen må ikke være mindre end 4 bogstaver")
            .MaximumLength(4096).WithMessage("Beskrivelse må ikke være mere end 4096 bogstaver lang");

        RuleFor(i => i.Images)
            .NotEmpty().WithMessage("Der mangler et billede")
            .Must(i => i.Count <= 10).WithMessage("Maksimal 10 billeder")
            .ForEach(image =>
            {
                // Maximum 20MB of ASCII characters
                image.MaximumLength(20_000_000 / 8).WithMessage("Billeder må højst være 20 MB");
                image.Must(i => Convert.TryFromBase64String(i, new(new byte[i.Length]), out _))
                    .WithMessage("Titlen er ikke i det rigtige format");
            });

        RuleFor(c => c.Category)
            .NotEmpty().WithMessage("Der mangler en kategori");
    }
}