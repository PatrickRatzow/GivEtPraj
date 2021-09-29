namespace Infrastructure.Persistence.Configurations
{
    public class CasePictureConfiguration : IEntityTypeConfiguration<CasePicture>
    {
        public void Configure(EntityTypeBuilder<CasePicture> builder)
        {
            builder.HasKey(cp => cp.Id);
        }
    }
}