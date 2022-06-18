using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public interface IReadRepository<TEntity>
    {
        Task<TEntity> GetAsync(string id);
    }
}