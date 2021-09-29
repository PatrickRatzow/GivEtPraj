using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Case> Cases { get; set; }
    DbSet<CasePicture> CasePictures { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}