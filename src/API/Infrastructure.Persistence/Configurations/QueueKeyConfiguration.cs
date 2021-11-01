namespace Infrastructure.Persistence.Configurations;

public class QueueKeyConfiguration : IEntityTypeConfiguration<QueueKey>
{
    public void Configure(EntityTypeBuilder<QueueKey> builder)
    {
        builder.Property(qk => qk.DeviceId)
            .IsRequired();

        builder.Property(qk => qk.CreatedAt)
            .ValueGeneratedOnAdd();

        builder.Property(qk => qk.ExpiresAt)
            .HasDefaultValue();
        
        builder.Property(qk => qk.CaptchaScore)
            .IsRequired();
    }
}