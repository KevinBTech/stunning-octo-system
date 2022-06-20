using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Domain.Queries;
using System.Collections.Generic;
using System.Linq;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed class ExpensesQuering
    {
        private readonly IReadRepository<Expense> _readExpensesRepository;

        public ExpensesQuering(IReadRepository<Expense> readExpensesRepository)
        {
            _readExpensesRepository = readExpensesRepository;
        }

        public IEnumerable<ExistingExpense> ListFrom(ExpensesSearch expensesSearch)
        {
            var existingExpenses = new List<ExistingExpense>();

            var queryBuilder = new ExpensesQueryBuilder(_readExpensesRepository)
                .FromIdentity(expensesSearch.IdentityId);

            if (expensesSearch.OrderBy.Name.Equals(
                nameof(Expense.Amount)
                , System.StringComparison.InvariantCultureIgnoreCase))
            {
                queryBuilder.OrderByAmount(expensesSearch.OrderBy.IsAscending);
            }

            foreach (Expense expense in queryBuilder.Build().ToList())
            {
                existingExpenses.Add(new ExistingExpense(
                    expense.OperationDate,
                    new Amount(expense.Amount, expense.Currency)
                    , expense.Nature
                    , expense.Comment
                    , expense.IdentityId));
            }

            return existingExpenses;
        }
    }
}