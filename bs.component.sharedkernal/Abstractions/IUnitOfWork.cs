using System;
using System.Threading;
using System.Threading.Tasks;

namespace bs.component.sharedkernal.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
