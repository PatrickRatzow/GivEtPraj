namespace Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasMany(category => category.Cases)
            .WithOne(@case => @case.Category);

        builder.HasMany(category => category.SubCategories)
            .WithOne(@case => @case.Category);
    }
}