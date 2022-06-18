using BTech.ExpenseSystem.Domain.Events;
using BTech.ExpenseSystem.Domain.UseCases;
using BTech.ExpenseSystem.WebAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BTech.ExpenseSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpensesCreator _expensesCreator;

        public ExpensesController(
            ExpensesCreator expensesCreator)
        {
            _expensesCreator = expensesCreator;
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
                , expenseToCreate.IdentityId);

            var @event = await _expensesCreator.ExecuteAsync(newExpense);

            return @event switch
            {
                ExpenseCreated => Ok(),
                _ => BadRequest(),
            };
        }
    }
}