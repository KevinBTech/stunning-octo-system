namespace BTech.ExpenseSystem.Domain.Events
{
    public interface IExpenseEvent
    {
    }

    public sealed record ExpenseCreated() : IExpenseEvent
    {
    }

    public sealed record NatureNotFound(string Message) : IExpenseEvent
    {
    }
}