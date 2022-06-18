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
            var repository = new InMemoryRepository<Expense>();
            var creator = new ExpensesCreator(repository);
            string identityId = Guid.NewGuid().ToString();

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow
                , new Amount(10, null)
                , ExpenseNature.Misc.ToString()
                , identityId));

            Assert.IsType<ExpenseCreated>(result);
        }

        [Fact]
        public async Task Execute_NewExpense_Without_ExistingNature_MustNotBeCreated()
        {
            var repository = new InMemoryRepository<Expense>();
            var creator = new ExpensesCreator(repository);
            string identityId = Guid.NewGuid().ToString();

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow
                , new Amount(10, null)
                , string.Empty
                , identityId));

            Assert.IsType<NatureNotFound>(result);
        }
    }
}