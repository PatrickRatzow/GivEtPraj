namespace Infrastructure.Persistence.Configurations;

public class BaseCaseConfiguration : IEntityTypeConfiguration<BaseCase>
{
    public void Configure(EntityTypeBuilder<BaseCase> builder)
    {
        builder.HasIndex(c => c.DeviceId);

        builder.HasMany(c => c.Images)
            .WithOne(cp => cp.Case)
            .HasForeignKey(cp => cp.CaseId);

        builder.Property(c => c.DeviceId)
            .IsRequired();

        builder.Property(c => c.Priority);

        builder.Property(c => c.IpAddress);
        builder.OwnsOne(c => c.GeographicLocation);

        builder.HasMany(c => c.CaseUpdates)
            .WithOne(cu => cu.BaseCase)
            .HasForeignKey(cu => cu.CaseId);
    }
}

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.Property(c => c.Comment)
            .HasMaxLength(4096)
            .IsRequired();
        builder.HasMany(c => c.SubCategories)
            .WithMany(subCat => subCat.Cases);
    }
}

public class MiscellaneousConfiguration : IEntityTypeConfiguration<MiscellaneousCase>
{
    public void Configure(EntityTypeBuilder<MiscellaneousCase> builder)
    {
        builder.Property(c => c.Description)
            .HasMaxLength(4096)
            .IsRequired();
    }
}