using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Domain.Events;
using System;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed class ExpensesCreator
    {
        private readonly IWriteRepository<Expense> _writeExpensesRepository;

        public ExpensesCreator(IWriteRepository<Expense> writeExpensesRepository)
        {
            _writeExpensesRepository = writeExpensesRepository;
        }

        public async Task<IExpenseEvent> ExecuteAsync(NewExpense newExpense)
        {
            var expenseToAdd = new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = newExpense.Amount.Value,
                OperationDate = newExpense.OperationDate,
                IdentityId = newExpense.IdentityId
            };

            await _writeExpensesRepository.AddAsync(expenseToAdd);

            return new ExpenseCreated();
        }
    }
}