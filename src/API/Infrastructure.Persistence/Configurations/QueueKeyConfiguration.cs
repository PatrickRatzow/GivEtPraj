namespace Infrastructure.Persistence.Configurations;

public class QueueKeyConfiguration : IEntityTypeConfiguration<RecaptchaAuthorization>
{
    public void Configure(EntityTypeBuilder<RecaptchaAuthorization> builder)
    {
        builder.Property(qk => qk.DeviceId)
            .IsRequired();

        builder.Property(qk => qk.ExpiresAt)
            .IsRequired();

        builder.Property(qk => qk.CaptchaScore)
            .IsRequired();
    }
}