using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public interface IWriteRepository<in TEntity>
    {
        Task AddAsync(TEntity entity);
    }
}