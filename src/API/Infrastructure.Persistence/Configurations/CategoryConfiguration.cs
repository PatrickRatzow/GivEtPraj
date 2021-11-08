namespace Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.OwnsOne(category => category.Name, name =>
        {
            name.Property(x => x.Danish)
                .IsRequired()
                .HasMaxLength(120);
            name.Property(x => x.English)
                .IsRequired()
                .HasMaxLength(120);
        });

        builder.Property(category => category.Miscellaneous)
            .HasDefaultValue(false);
        
        builder.Property(category => category.Icon)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasMany(category => category.Cases)
            .WithOne(@case => @case.Category);

        builder.HasMany(category => category.SubCategories)
            .WithOne(@case => @case.Category);
    }
}