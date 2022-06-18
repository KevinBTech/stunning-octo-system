using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Domain.Enums;
using BTech.ExpenseSystem.Domain.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed class ExpensesCreator
    {
        private readonly IWriteRepository<Expense> _writeExpensesRepository;
        private readonly IReadRepository<User> _readUsersRepository;

        public ExpensesCreator(IWriteRepository<Expense> writeExpensesRepository
            , IReadRepository<User> readUsersRepository)
        {
            _writeExpensesRepository = writeExpensesRepository;
            _readUsersRepository = readUsersRepository;
        }

        public async Task<IExpenseEvent> ExecuteAsync(NewExpense newExpense)
        {
            if (!Enum.TryParse(newExpense.Nature
                , true
                , out ExpenseNature parsedNature))
            {
                return new NatureNotFound("The given nature is not recognized." +
                    $" Accepted values are {string.Join(", ", Enum.GetValues<ExpenseNature>())}.");
            }

            if (string.IsNullOrWhiteSpace(newExpense.Comment))
            {
                return new CommentIsMandatory("A comment is mandatory.");
            }

            if (!_readUsersRepository.Entities
                .Any(u => newExpense.IdentityId == u.Id))
            {
                return new IdentityUnknown($"The user '{newExpense.IdentityId}' is unkown.");
            }

            var expenseToAdd = new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = newExpense.Amount.Value,
                Currency = newExpense.Amount.Currency,
                OperationDate = newExpense.OperationDate,
                Nature = parsedNature.ToString(),
                Comment = newExpense.Comment,
                IdentityId = newExpense.IdentityId
            };

            await _writeExpensesRepository.AddAsync(expenseToAdd);

            return new ExpenseCreated();
        }
    }
}