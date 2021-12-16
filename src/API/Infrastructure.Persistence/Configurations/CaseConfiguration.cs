namespace Infrastructure.Persistence.Configurations;

public class BaseCaseConfiguration : IEntityTypeConfiguration<BaseCase>
{
    public void Configure(EntityTypeBuilder<BaseCase> builder)
    {
        builder.HasMany(c => c.Images)
            .WithOne();

        builder.HasOne(c => c.Category)
            .WithMany(c => c.Cases);

        builder.Property(c => c.DeviceId)
            .IsRequired();

        builder.OwnsOne(c => c.GeographicLocation);

        builder.Property(c => c.CreatedAt)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UpdatedAt)
            .ValueGeneratedOnUpdate();

        builder.HasMany(c => c.CaseUpdates)
            .WithOne(cu => cu.BaseCase)
            .HasForeignKey(cu => cu.Id);
    }
}

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.Property(c => c.Comment)
            .HasMaxLength(4096)
            .IsRequired();
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