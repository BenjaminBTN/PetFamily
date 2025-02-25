using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Database;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken);
    Task SaveChanges(CancellationToken cancellationToken);
}
