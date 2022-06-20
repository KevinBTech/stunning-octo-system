using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Domain.UseCases;
using BTech.ExpenseSystem.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BTech.ExpenseSytem.UnitTests
{
    public class ExpensesQueringUnitTest
    {
        [Fact]
        public async Task ListFrom_WithDescendingAmountOrder()
        {
            string identityId = "Harry Potter";
            var userRepository = new InMemoryRepository<User>();
            await userRepository.AddAsync(new User()
            {
                Id = identityId,
                FirstName = "Harry",
                LastName = "Potter",
                Currency = "Witch money"
            });
            var expenseRepository = new InMemoryRepository<Expense>();
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 10,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now.AddDays(-2),
                Currency = "Witch money"
            });
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 15,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now,
                Currency = "Witch money"
            });

            var quering = new ExpensesQuering(expenseRepository);

            var result = quering.ListFrom(new ExpensesSearch());

            Assert.Equal(15, result.First().Amount.Value);
        }

        [Fact]
        public async Task ListFrom_WithAscendingAmountOrder()
        {
            string identityId = "Harry Potter";
            var userRepository = new InMemoryRepository<User>();
            await userRepository.AddAsync(new User()
            {
                Id = identityId,
                FirstName = "Harry",
                LastName = "Potter",
                Currency = "Witch money"
            });
            var expenseRepository = new InMemoryRepository<Expense>();
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 10,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now.AddDays(-2),
                Currency = "Witch money"
            });
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 3,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now,
                Currency = "Witch money"
            });
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 15,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now,
                Currency = "Witch money"
            });

            var quering = new ExpensesQuering(expenseRepository);

            var result = quering.ListFrom(new ExpensesSearch()
            {
                OrderBy = new OrderBy("Amount", true)
            });

            Assert.Equal(3, result.First().Amount.Value);
        }

        [Fact]
        public async Task ListFrom_WithAscendingOperationOrder()
        {
            string identityId = "Harry Potter";
            var userRepository = new InMemoryRepository<User>();
            await userRepository.AddAsync(new User()
            {
                Id = identityId,
                FirstName = "Harry",
                LastName = "Potter",
                Currency = "Witch money"
            });
            var expenseRepository = new InMemoryRepository<Expense>();
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 10,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now.AddDays(-2),
                Currency = "Witch money"
            });
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 30,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now.AddDays(-5),
                Currency = "Witch money"
            });
            await expenseRepository.AddAsync(new Expense()
            {
                Id = Guid.NewGuid().ToString(),
                Amount = 15,
                Comment = "Experliammmmmmmmmuuuuus !",
                IdentityId = identityId,
                Nature = "Misc",
                OperationDate = DateTimeOffset.Now,
                Currency = "Witch money"
            });

            var quering = new ExpensesQuering(expenseRepository);

            var result = quering.ListFrom(new ExpensesSearch()
            {
                OrderBy = new OrderBy("OperationDate", true)
            });

            Assert.Equal(30, result.First().Amount.Value);
        }
    }
}