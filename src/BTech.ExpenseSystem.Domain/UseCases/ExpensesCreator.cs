using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Domain.Enums;
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
            if (!Enum.TryParse(newExpense.Nature
                , out ExpenseNature parsedNature))
            {
                return new NatureNotFound("The given nature is not recognized." +
                    $"Accepted values are {string.Join(",", Enum.GetValues<ExpenseNature>())}");
            }

            var expenseToAdd = new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = newExpense.Amount.Value,
                Currency = newExpense.Amount.Currency,
                OperationDate = newExpense.OperationDate,
                Nature = parsedNature.ToString(),
                IdentityId = newExpense.IdentityId
            };

            await _writeExpensesRepository.AddAsync(expenseToAdd);

            return new ExpenseCreated();
        }
    }
}