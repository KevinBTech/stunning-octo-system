using System;

namespace BTech.ExpenseSystem.Domain.Entities
{
    public sealed class Expense
    {
        public string Id { get; set; } = null!;
        public DateTimeOffset OperationDate { get; set; }
        public decimal Amount { get; set; }
        public string Nature { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public string IdentityId { get; set; } = null!;
    }
}