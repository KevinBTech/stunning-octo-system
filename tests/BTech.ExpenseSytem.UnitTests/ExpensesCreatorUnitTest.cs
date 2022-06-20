using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Domain.Enums;
using BTech.ExpenseSystem.Domain.Events;
using BTech.ExpenseSystem.Domain.UseCases;
using BTech.ExpenseSystem.Infrastructure.Data;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BTech.ExpenseSytem.UnitTests
{
    public class ExpensesCreatorUnitTest
    {
        [Fact]
        public async Task Execute_NewExpense_MustBeCreated()
        {
            var expenseRepository = new InMemoryRepository<Expense>();
            var userRepository = new InMemoryRepository<User>();
            await userRepository.AddAsync(new User()
            {
                Id = "Harry Potter",
                FirstName = "Harry",
                LastName = "Potter",
                Currency = "Witch money"
            });
            var creator = new ExpensesCreator(
                expenseRepository
                , userRepository
                , expenseRepository);
            string identityId = "Harry Potter";

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow
                , new Amount(10, null)
                , ExpenseNature.Misc.ToString()
                , "Expelliarmus !"
                , identityId));

            Assert.IsType<ExpenseCreated>(result);
        }

        [Fact]
        public async Task Execute_NewExpense_Without_ExistingNature_MustNotBeCreated()
        {
            var expenseRepository = new InMemoryRepository<Expense>();
            var creator = new ExpensesCreator(
                expenseRepository
                , new InMemoryRepository<User>()
                , expenseRepository);
            string identityId = "Harry Potter";

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow
                , new Amount(10, null)
                , string.Empty
                , string.Empty
                , identityId));

            Assert.IsType<NatureNotFound>(result);
        }

        [Fact]
        public async Task Execute_NewExpense_Without_Comment_MustNotBeCreated()
        {
            var expenseRepository = new InMemoryRepository<Expense>();
            var creator = new ExpensesCreator(
                expenseRepository
                , new InMemoryRepository<User>()
                , expenseRepository);
            string identityId = "Harry Potter";

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow
                , new Amount(10, null)
                , "Misc"
                , ""
                , identityId));

            Assert.IsType<CommentIsMandatory>(result);
        }

        [Fact]
        public async Task Execute_NewExpense_Without_KownIdentity_MustNotBeCreated()
        {
            var expenseRepository = new InMemoryRepository<Expense>();
            var creator = new ExpensesCreator(
                expenseRepository
                , new InMemoryRepository<User>()
                , expenseRepository);
            string identityId = "Harry Potter";

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow
                , new Amount(10, null)
                , "Misc"
                , "Sperooooooo patronuuuuuuuuuuuuum "
                , identityId));

            Assert.IsType<IdentityUnknown>(result);
        }

        [Fact]
        public async Task Execute_NewExpense_CanNotHaveADateInFutur_MustFail()
        {
            var expenseRepository = new InMemoryRepository<Expense>();
            var userRepository = new InMemoryRepository<User>();
            await userRepository.AddAsync(new User()
            {
                Id = "Harry Potter",
                FirstName = "Harry",
                LastName = "Potter",
                Currency = "Witch money"
            });
            var creator = new ExpensesCreator(
                expenseRepository
                , userRepository,
                expenseRepository);
            string identityId = "Harry Potter";

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow.AddDays(1)
                , new Amount(10, null)
                , ExpenseNature.Misc.ToString()
                , "Expelliarmus !"
                , identityId));

            Assert.IsType<CanNotHaveDateInFutur>(result);
        }

        [Fact]
        public async Task Execute_NewExpense_SameExpenseAlreadyExists_MustFail()
        {
            string identityId = "Harry Potter";
            var expenseRepository = new InMemoryRepository<Expense>();
            var userRepository = new InMemoryRepository<User>();
            await userRepository.AddAsync(new User()
            {
                Id = identityId,
                FirstName = "Harry",
                LastName = "Potter",
                Currency = "Witch money"
            });
            var newExpense = new NewExpense(
                DateTimeOffset.UtcNow
                , new Amount(10, null)
                , ExpenseNature.Misc.ToString()
                , "Expelliarmus !"
                , identityId);
            await expenseRepository.AddAsync(new Expense()
            {
                OperationDate = newExpense.OperationDate,
                Amount = newExpense.Amount.Value,
                Currency = newExpense.Amount.Currency,
                Nature = newExpense.Nature,
                Comment = newExpense.Comment,
                IdentityId = newExpense.IdentityId
            });

            var creator = new ExpensesCreator(
                expenseRepository
                , userRepository
                , expenseRepository);

            var result = await creator.ExecuteAsync(newExpense);

            Assert.IsType<SameExpenseAlreadyExists>(result);
        }
    }
}