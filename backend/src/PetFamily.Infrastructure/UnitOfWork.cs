using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Application.Database;
using System.Data;

namespace PetFamily.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return transaction.GetDbTransaction();
        }

        public async Task SaveChanges(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
