using System;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed record ExistingExpense(
        DateTimeOffset OperationDate
        , Amount Amount
        , string Nature
        , string Comment
        , string IdentityId)
    {
    }
}