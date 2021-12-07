using System.Reflection;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<BaseCase> Cases { get; set; } = null!;
    public DbSet<CaseImage> Images { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<SubCategory> SubCategories { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<ReCaptchaAuthorization> QueueKeys { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}