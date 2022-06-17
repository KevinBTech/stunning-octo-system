using BTech.ExpenseSystem.Domain.Events;
using BTech.ExpenseSystem.Domain.UseCases;
using System;
using Xunit;

namespace BTech.ExpenseSytem.UnitTests
{
    public class ExpensesCreatorUnitTest
    {
        [Fact]
        public void Execute_With_Date_Amount_Identity_MustBeCreated()
        {
            var creator = new ExpensesCreator();
            string identityId = Guid.NewGuid().ToString();

            var result = creator.Execute(new NewExpense(
                DateTimeOffset.UtcNow
                , 20
                , identityId));

            Assert.Equal(typeof(ExpenseCreated), result.GetType());
        }
    }
}