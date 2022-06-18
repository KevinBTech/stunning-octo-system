using BTech.ExpenseSystem.Domain.Entities;
using BTech.ExpenseSystem.Infrastructure.Data.Ef.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BTech.ExpenseSystem.Infrastructure.Data
{
    public sealed class ExpenseSystemContext : DbContext
    {
        #region Properties

        public DbSet<Expense> Expenses { get; set; } = null!;

        #endregion Properties

        public ExpenseSystemContext(DbContextOptions<ExpenseSystemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExpenseMapping());
        }
    }
}