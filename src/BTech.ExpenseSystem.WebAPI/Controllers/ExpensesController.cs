using BTech.ExpenseSystem.Domain.Events;
using BTech.ExpenseSystem.Domain.UseCases;
using BTech.ExpenseSystem.WebAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpensesCreator _expensesCreator;
        private readonly ExpensesQuering _expensesQuering;

        public ExpensesController(
            ExpensesCreator expensesCreator
            , ExpensesQuering expensesQuering)
        {
            _expensesCreator = expensesCreator;
            _expensesQuering = expensesQuering;
        }

        /// <summary>
        /// To create a new expense.
        /// </summary>
        /// <param name="expenseToCreate"><see cref="ExpensesCreator"/></param>
        [HttpPost]
        public async Task<IActionResult> Create(
            [Required] ExpenseToCreate expenseToCreate)
        {
            var newExpense = new NewExpense(expenseToCreate.OperationDate
                , new Amount(
                    expenseToCreate.Amount
                    , expenseToCreate.Currency)
                , expenseToCreate.Nature
                , expenseToCreate.Comment
                , expenseToCreate.IdentityId);

            var @event = await _expensesCreator.ExecuteAsync(newExpense);

            return @event switch
            {
                ExpenseCreated => Ok(),
                IExpenseToCreateInErrorEvent expenseToCreateInErrorEvent => BadRequest(expenseToCreateInErrorEvent.Message),
                _ => BadRequest(),
            };
        }

        /// <summary>
        /// List existing expenses from search.
        /// </summary>
        /// <param name="expensesFilter"><see cref="ExpensesSearch"/></param>
        [HttpGet]
        public ActionResult<ExpensesList> Get(
            [Required][FromQuery] ExpensesFilter expensesFilter)
        {
            var existingExpenses = _expensesQuering.ListFrom(new ExpensesSearch()
            {
                IdentityId = expensesFilter.IdentityId,
                OrderBy = new OrderBy(expensesFilter.OrderByName
                , expensesFilter.IsAscendingOrder)
            });

            var expensesList = new ExpensesList
            {
                Expenses = existingExpenses.Select(e =>
                new Expense()
                {
                    OperationDate = e.OperationDate,
                    Amount = e.Amount.Value,
                    Currency = e.Amount.Currency,
                    Comment = e.Comment,
                    IdentityId = e.IdentityId,
                    Nature = e.Nature
                })
            };

            return expensesList;
        }
    }
}