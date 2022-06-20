namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed class ExpensesSearch
    {
        public string? IdentityId { get; set; }

        public OrderBy OrderBy { get; set; } = new OrderBy(nameof(ExistingExpense.Amount), false);
    }
}