using System;
using System.Collections.Generic;

namespace Commentor.GivEtPraj.Application.Contracts;

public class CaseCreationDto
{
    public CaseCreationDto()
    {
    }

    public CaseCreationDto(List<string> images, Guid category, double longitude,
        double latitude, Guid[]? subCategories, string? description = null, string? comment = null)
    {
        Description = description;
        Comment = comment;
        SubCategoryIds = subCategories;
        Images = images;
        CategoryId = category;
        Longitude = longitude;
        Latitude = latitude;
    }

    public string? Description { get; set; }
    public string? Comment { get; set; }
    public Guid[]? SubCategoryIds { get; set; }
    public List<string> Images { get; set; } = new();
    public Guid CategoryId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}