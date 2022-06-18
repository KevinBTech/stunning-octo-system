using System;
using System.Collections.Generic;

namespace BTech.ExpenseSystem.WebAPI.Contracts
{
    public sealed class ExpensesList
    {
        /// <summary>
        /// The identity of the user who make the expense.
        /// </summary>
        public IEnumerable<Expense> Expenses { get; set; } = new List<Expense>();
    }

    public sealed class Expense
    {
        public DateTimeOffset OperationDate { get; set; }
        public decimal Amount { get; set; }
        public string Nature { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string IdentityId { get; set; } = null!;
    }
}