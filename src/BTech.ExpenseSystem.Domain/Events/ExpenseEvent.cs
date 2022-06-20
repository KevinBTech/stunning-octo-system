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

    public sealed record IdentityUnknown(string Message) : IExpenseToCreateInErrorEvent
    {
    }

    public sealed record CanNotHaveDateInFutur(string Message) : IExpenseToCreateInErrorEvent
    {
    }

    public sealed record CanNotHaveADateOlderThan3Months(string Message) : IExpenseToCreateInErrorEvent
    {
    }

    public sealed record SameExpenseAlreadyExists(string Message) : IExpenseToCreateInErrorEvent
    {
    }
}