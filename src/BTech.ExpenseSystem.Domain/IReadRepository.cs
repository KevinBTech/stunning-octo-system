using System.Linq;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public interface IReadRepository<out TEntity>
    {
        IQueryable<TEntity> Entities { get; }
    }
}