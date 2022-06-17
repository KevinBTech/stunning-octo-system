using System;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed record NewExpense(
        DateTimeOffset OperationDate
        , decimal Amount
        , string IdentityId)
    {
    }
}