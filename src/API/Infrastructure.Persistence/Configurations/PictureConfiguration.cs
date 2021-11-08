namespace Infrastructure.Persistence.Configurations;

public class PictureConfiguration : IEntityTypeConfiguration<CaseImage>
{
    public void Configure(EntityTypeBuilder<CaseImage> builder)
    {
        builder.HasKey(cp => cp.Id);
    }
}