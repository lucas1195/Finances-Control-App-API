namespace Finances_Control_App_API.DTO
{
    public class FinancialPlanLogsResponse
    {
        public int FanincialPlanId { get; set; }
        public string FanincialPlanName { get; set; }
        public DateTime PlanStartDate { get; set; }
        public int TransferId { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal TransferAmount { get; set; }
        public string? TransferDescription { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? AccountFlagId { get; set; }
        public string? AccountFlagName { get; set; }

    }
}
