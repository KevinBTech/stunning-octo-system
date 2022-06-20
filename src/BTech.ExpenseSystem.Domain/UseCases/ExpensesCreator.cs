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
        private readonly IReadRepository<Expense> _readExpensesRepository;

        public ExpensesCreator(IWriteRepository<Expense> writeExpensesRepository
            , IReadRepository<User> readUsersRepository
            , IReadRepository<Expense> readExpensesRepository)
        {
            _writeExpensesRepository = writeExpensesRepository;
            _readUsersRepository = readUsersRepository;
            _readExpensesRepository = readExpensesRepository;
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

            var user = _readUsersRepository.Entities
                .FirstOrDefault(u => newExpense.IdentityId == u.Id);

            if (user is null)
            {
                return new IdentityUnknown($"The user '{newExpense.IdentityId}' is unkown.");
            }

            if (newExpense.OperationDate > DateTimeOffset.Now)
            {
                return new CanNotHaveDateInFutur("An expense can not be in the futur.");
            }

            if (newExpense.OperationDate < DateTimeOffset.Now.AddMonths(-3))
            {
                return new CanNotHaveADateOlderThan3Months("An expense can not be older than 3 months.");
            }

            if (_readExpensesRepository.Entities
                .Any(e => e.IdentityId == user.Id
                && e.Amount == newExpense.Amount.Value
                && e.OperationDate == newExpense.OperationDate))
            {
                return new SameExpenseAlreadyExists("A similary expense already exists : same amount on the same operation date.");
            }

            if (!user.Currency.Equals(
                newExpense.Amount.Currency
                , StringComparison.InvariantCultureIgnoreCase))
            {
                return new IdentityCurrencyIsNotIdentical("The given currency is not the same as the given identity : it must be identical.");
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