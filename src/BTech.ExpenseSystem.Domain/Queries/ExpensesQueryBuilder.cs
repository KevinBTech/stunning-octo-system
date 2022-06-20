using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Domain.UseCases;
using System.Linq;

namespace BTech.ExpenseSystem.Domain.Queries
{
    internal sealed class ExpensesQueryBuilder
    {
        private IQueryable<Expense> _query;

        public ExpensesQueryBuilder(IReadRepository<Expense> readRepository)
        {
            _query = readRepository.Entities;
        }

        public IQueryable<Expense> Build() => _query;

        public ExpensesQueryBuilder FromIdentity(string? identity)
        {
            if (!string.IsNullOrEmpty(identity))
            {
                _query = _query.Where(e => e.IdentityId == identity);
            }

            return this;
        }

        public ExpensesQueryBuilder OrderByAmount(bool isAscending = false)
        {
            if (isAscending)
            {
                _query = _query.OrderBy(e => e.Amount);
            }
            else
            {
                _query = _query.OrderByDescending(e => e.Amount);
            }

            return this;
        }
    }
}