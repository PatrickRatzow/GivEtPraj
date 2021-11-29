using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Commentor.GivEtPraj.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<BaseCase> Cases { get; set; }
    DbSet<CaseImage> Images { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<SubCategory> SubCategories { get; set; }
    DbSet<ReCaptchaAuthorization> QueueKeys { get; set; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}