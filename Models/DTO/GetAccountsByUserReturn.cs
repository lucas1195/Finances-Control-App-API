using Finances_Control_App.Domain.FinancesApp.Enums;

namespace Finances_Control_App_API.Models.DTO
{
    public class GetAccountsByUserReturn
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string InstitutionName { get; set; }
        public string? AgencyNumber { get; set; }
        public string? AccountNumber { get; set; }
        public double Balance { get; set; }
        public int? AccountFlagId { get; set; }
        public string? AccountFlagName { get; set; }
        public AccountType? TransactionType { get; set; }
    }
}
