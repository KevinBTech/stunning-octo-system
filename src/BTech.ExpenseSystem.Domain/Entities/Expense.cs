using System;

namespace BTech.ExpenseSystem.Domain.Entities
{
    public sealed class Expense
    {
        public string Id { get; set; }

        public DateTimeOffset OperationDate { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public string IdentityId { get; set; }
    }
}