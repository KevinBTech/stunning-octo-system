using System;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed record NewExpense(
        DateTimeOffset OperationDate
        , Amount Amount
        , string Nature
        , string IdentityId)
    {
    }
}