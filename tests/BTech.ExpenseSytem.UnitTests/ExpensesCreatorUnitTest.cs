using BTech.ExpenseSystem.Domain.Entities;
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
        public async Task Execute_With_Date_Amount_Identity_MustBeCreated()
        {
            var repository = new InMemoryRepository<Expense>();
            var creator = new ExpensesCreator(repository);
            string identityId = Guid.NewGuid().ToString();

            var result = await creator.ExecuteAsync(new NewExpense(
                DateTimeOffset.UtcNow
                , 20
                , identityId));

            Assert.IsType<ExpenseCreated>(result);
        }
    }
}