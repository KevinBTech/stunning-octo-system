namespace BTech.ExpenseSystem.WebAPI.Contracts
{
    public sealed class ExpensesFilter
    {
        /// <summary>
        /// The identity of the user who make the expense.
        /// </summary>
        public string? IdentityId { get; set; } = null!;
    }
}