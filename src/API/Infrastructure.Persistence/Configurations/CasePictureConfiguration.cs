using Commentor.GivEtPraj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CasePictureConfiguration : IEntityTypeConfiguration<CasePicture>
    {
        public void Configure(EntityTypeBuilder<CasePicture> builder)
        {
            builder.Property(cp => cp.ImageData)
                .IsRequired();
        }
    }
}