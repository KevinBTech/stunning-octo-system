using BTech.ExpenseSystem.Domain.Entities;
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

            foreach (Expense expense in _readExpensesRepository.Entities.ToList())
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