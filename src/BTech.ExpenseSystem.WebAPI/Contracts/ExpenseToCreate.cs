using System;
using System.ComponentModel.DataAnnotations;

namespace BTech.ExpenseSystem.WebAPI.Contracts
{
    public sealed class ExpenseToCreate
    {
        [Required]
        public DateTimeOffset OperationDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; } = null!;

        public string Nature { get; set; } = null!;

        [Required]
        public string IdentityId { get; set; } = null!;
    }
}