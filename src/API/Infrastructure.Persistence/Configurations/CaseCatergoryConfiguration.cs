namespace Infrastructure.Persistence.Configurations;

public class CaseCatergoryConfiguration : IEntityTypeConfiguration<CaseCategory>
{
    public void Configure(EntityTypeBuilder<CaseCategory> builder)
    {
        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasMany(category => category.Cases)
            .WithMany(@case => @case.Categories);
    }
}