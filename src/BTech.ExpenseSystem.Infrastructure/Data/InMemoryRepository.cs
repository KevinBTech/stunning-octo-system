using BTech.ExpenseSystem.Domain.UseCases;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Infrastructure.Data
{
    public class InMemoryRepository<TEntity>
            : IWriteRepository<TEntity>
        , IReadRepository<TEntity>

    {
        #region Properties

        private readonly IDictionary<string, List<TEntity>> _entities;

        #endregion Properties

        public InMemoryRepository()
        {
            _entities = new Dictionary<string, List<TEntity>>
            {
                { typeof(TEntity).Name, new List<TEntity>() }
            };
        }

        public IQueryable<TEntity> Entities => _entities[typeof(TEntity).Name].AsQueryable();

        public Task AddAsync(TEntity entity)
        {
            _entities[typeof(TEntity).Name].Add(entity);

            return Task.CompletedTask;
        }
    }
}