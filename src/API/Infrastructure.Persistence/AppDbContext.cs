using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Case> Cases { get; set; }
        public DbSet<CasePicture> CasePictures { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}