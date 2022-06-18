namespace BTech.ExpenseSystem.Domain.Events
{
    public interface IExpenseEvent
    {
    }

    public interface IExpenseToCreateInErrorEvent : IExpenseEvent
    {
        string Message { get; }
    }

    public sealed record ExpenseCreated() : IExpenseEvent
    {
    }

    public sealed record NatureNotFound(string Message) : IExpenseToCreateInErrorEvent
    {
    }

    public sealed record CommentIsMandatory(string Message) : IExpenseToCreateInErrorEvent
    {
    }
}