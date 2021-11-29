namespace Infrastructure.Persistence.Configurations;

public class QueueKeyConfiguration : IEntityTypeConfiguration<ReCaptchaAuthorization>
{
    public void Configure(EntityTypeBuilder<ReCaptchaAuthorization> builder)
    {
        builder.Property(qk => qk.DeviceId)
            .IsRequired();

        builder.Property(qk => qk.ExpiresAt)
            .IsRequired();

        builder.Property(qk => qk.CaptchaScore)
            .IsRequired();
    }
}