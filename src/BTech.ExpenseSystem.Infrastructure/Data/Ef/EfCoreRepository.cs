using BTech.ExpenseSystem.Domain.UseCases;
using System.Linq;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Infrastructure.Data
{
    internal sealed class EfRepository<TEntity>

        : IWriteRepository<TEntity>,
        IReadRepository<TEntity>
        where TEntity : class
    {
        private readonly ExpenseSystemContext _context;

        public EfRepository(ExpenseSystemContext expenseSystemContext)
        {
            _context = expenseSystemContext;
        }

        public IQueryable<TEntity> Entities => _context.Set<TEntity>();

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
    }
}