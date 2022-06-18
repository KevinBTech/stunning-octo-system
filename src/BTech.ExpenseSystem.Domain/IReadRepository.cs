using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public interface IReadRepository<TEntity>
    {
        IQueryable<TEntity> Entities { get; }
    }
}