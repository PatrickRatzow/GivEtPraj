namespace Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(c => c.Id)
            .IsRequired();


        builder.Property(c => c.FirstName)
            .IsRequired();
            

        builder.Property(c => c.LastName)
            .IsRequired();
    }
}

