namespace BTech.ExpenseSystem.Domain.Events
{
    public abstract record ExpenseEvent
    {
    }

    public sealed record ExpenseCreated() : ExpenseEvent
    {
    }

    public sealed record NewExpenseInError() : ExpenseEvent
    {
    }
}