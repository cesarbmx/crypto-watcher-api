using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CryptoWatcher.Shared.Contexts
{
    public interface IContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}