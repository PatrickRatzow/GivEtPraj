using System.Threading;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Case> Cases { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}