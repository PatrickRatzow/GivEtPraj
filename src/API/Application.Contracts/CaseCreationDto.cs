using Commentor.GivEtPraj.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commentor.GivEtPraj.Application.Contracts
{
    public class CaseCreationDto
    {
        public CaseCreationDto()
        {
        }

        public CaseCreationDto(List<string> images, int category, double longitude,
            double latitude, string? description = null, string? comment = null, int[]? subCategories = null)
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
        public int[]? SubCategoryIds { get; set; }
        public List<string> Images { get; set; } = new();
        public int CategoryId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
