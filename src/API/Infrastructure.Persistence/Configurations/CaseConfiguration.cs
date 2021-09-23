using Commentor.GivEtPraj.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.Property(c => c.Title)
                .HasMaxLength(127)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(4096)
                .IsRequired();

            builder.HasMany(c => c.Pictures)
                .WithOne(cp => cp.Case)
                .HasForeignKey(cp => cp.CaseId);
        }
    }
}