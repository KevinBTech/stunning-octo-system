using BTech.ExpenseSystem.Domain.Events;

namespace BTech.ExpenseSystem.Domain.UseCases
{
    public sealed class ExpensesCreator
    {
        public ExpenseEvent Execute(NewExpense newExpense)
        {
            return new NewExpenseInError();
        }
    }
}