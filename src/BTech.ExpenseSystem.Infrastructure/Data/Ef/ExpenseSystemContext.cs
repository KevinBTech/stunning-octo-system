using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Infrastructure.Data.Ef.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace BTech.ExpenseSystem.Infrastructure.Data
{
    public sealed class ExpenseSystemContext : DbContext
    {
        #region Properties

        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        #endregion Properties

        public ExpenseSystemContext(DbContextOptions<ExpenseSystemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());

            SeedFixtures(modelBuilder);
        }

        private void SeedFixtures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = "Anthony Stark",
                    FirstName = "Anthony",
                    LastName = "Stark",
                    Currency = "USD"
                });
            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    Id = "Natasha Romanova",
                    FirstName = "Natasha",
                    LastName = "Romanova",
                    Currency = "RUB"
                });
        }
    }
}