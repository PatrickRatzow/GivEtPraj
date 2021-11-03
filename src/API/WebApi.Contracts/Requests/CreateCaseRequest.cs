using FluentValidation;

namespace Commentor.GivEtPraj.WebApi.Contracts.Requests;

public class CreateCaseRequest
{
    public CreateCaseRequest()
    {
    }

    public CreateCaseRequest(string title, List<string> images, string category, double longitude,
        double latitude, Guid deviceId, string description = "", string comment = "", string?[] subCategories = null)
    {
        Title = title;
        Description = description;
        Comment = comment;
        SubCategories = subCategories;  
        Images = images;
        Category = category;
        Longitude = longitude;
        Latitude = latitude;
        DeviceId = deviceId;
    }

    public string Title { get; set; }
    public Guid DeviceId { get; set; }
    public string Description { get; set; }
    public string Comment { get; set; }
    public string?[] SubCategories { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public string Category { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}