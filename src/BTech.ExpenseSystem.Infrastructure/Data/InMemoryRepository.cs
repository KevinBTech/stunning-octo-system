using BTech.ExpenseSystem.Domain.UseCases;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Infrastructure.Data
{
    public class InMemoryRepository<TEntity>
            : IWriteRepository<TEntity>
    {
        #region Properties

        private readonly IList<TEntity> _entities;

        #endregion Properties

        public InMemoryRepository()
        {
            _entities = new List<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            _entities.Add(entity);

            return Task.CompletedTask;
        }
    }
}