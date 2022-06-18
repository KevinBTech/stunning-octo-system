using BTech.ExpenseSystem.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BTech.ExpenseSystem.WebAPI.Contracts
{
    /// <summary>
    /// Properties of an new expense.
    /// </summary>
    public sealed class ExpenseToCreate
    {
        /// <summary>
        /// The date of the expense.
        /// </summary>
        [Required]
        public DateTimeOffset OperationDate { get; set; }

        /// <summary>
        /// An amount
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// The currency.
        /// </summary>
        [Required]
        public string Currency { get; set; } = null!;

        /// <summary>
        /// The associated comment.
        /// </summary>
        [Required]
        public string Comment { get; set; } = null!;

        /// <summary>
        /// The nature of the expense.
        /// </summary>
        public string Nature { get; set; } = ExpenseNature.Misc.ToString();

        /// <summary>
        /// The identity of the user who make the expense.
        /// </summary>
        [Required]
        public string IdentityId { get; set; } = null!;
    }
}