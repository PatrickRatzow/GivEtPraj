namespace Infrastructure.Persistence.Configurations;

public class ReCaptchaAuthorizationConfiguration : IEntityTypeConfiguration<ReCaptchaAuthorization>
{
    public void Configure(EntityTypeBuilder<ReCaptchaAuthorization> builder)
    {
        builder.HasKey(x => x.DeviceId);        

        builder.Property(x => x.ExpiresAt)
            .IsRequired();
    }
}