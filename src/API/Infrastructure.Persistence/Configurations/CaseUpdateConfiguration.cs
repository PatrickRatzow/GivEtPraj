namespace Infrastructure.Persistence.Configurations;
internal class CaseUpdateConfiguration : IEntityTypeConfiguration<CaseUpdate>
{
    public void Configure(EntityTypeBuilder<CaseUpdate> builder)
    {

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.CurrentStatus);

        builder.HasOne(c => c.Employee)
            .WithMany();

        builder.Property(c => c.SendToReporter);

    }
}

