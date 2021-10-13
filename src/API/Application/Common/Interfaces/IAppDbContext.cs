using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Case> Cases { get; set; }
    DbSet<Picture> Pictures { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Employee> Employees {  get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}