namespace Infrastructure.Persistence.Configurations;

internal class CaseUpdateConfiguration : IEntityTypeConfiguration<CaseUpdate>
{
    public void Configure(EntityTypeBuilder<CaseUpdate> builder)
    {

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.Status);

        builder.Property(c => c.SendToReporter);

    }
}