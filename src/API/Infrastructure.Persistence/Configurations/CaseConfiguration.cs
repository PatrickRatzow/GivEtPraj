using System.Net;

namespace Infrastructure.Persistence.Configurations;

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {

        builder.Property(c => c.Description)
            .HasMaxLength(4096)
            .IsRequired();

        builder.HasMany(c => c.Pictures)
            .WithOne(cp => cp.Case)
            .HasForeignKey(cp => cp.CaseId);

        builder.OwnsOne(c => c.GeographicLocation);

        builder.Property(c => c.IpAddress)
            .IsRequired()
            .HasConversion(
                c => c.ToString(),
                c => IPAddress.Parse(c));

        builder.Property(c => c.Priority);

    }
}