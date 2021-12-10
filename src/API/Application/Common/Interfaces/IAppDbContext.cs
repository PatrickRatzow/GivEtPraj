using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Commentor.GivEtPraj.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<BaseCase> Cases { get; set; }
    DbSet<CaseImage> Images { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<SubCategory> SubCategories { get; set; }
    DbSet<ReCaptchaAuthorization> PreAuthorizations { get; set; }
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}