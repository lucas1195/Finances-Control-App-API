using Finances_Control_App.Domain.FinancesApp.Enums;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Finances_Control_App_API.Models.DTO
{
    public class AccountDTO
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }

        [StringLength(100, ErrorMessage = "The agency name must have a maximum of 100 characters.")]
        public string? AgencyNumber { get; set; }

        [StringLength(100, ErrorMessage = "The account number must have a maximum of 100 characters.")]
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "The account name is required.")]
        [StringLength(100, ErrorMessage = "The account name must have a maximum of 100 characters.")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "The institution name is required.")]
        [StringLength(100, ErrorMessage = "The institution name must have a maximum of 100 characters.")]
        public string InstitutionName { get; set; }

        [Required(ErrorMessage = "The balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The balance must be greater than or equal to zero.")]
        public double Balance { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The account flag must be a positive value.")]
        public int? AccountFlagId { get; set; }

        public AccountType? TransactionType { get; set; }
    }
}
