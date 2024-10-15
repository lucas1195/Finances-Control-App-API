namespace Finances_Control_App_API.Models.DTO
{
    public class TransferenciaDTO
    {
        public int? TransferId { get; set; }
        public decimal? TransferAmount { get; set; }
        public string? TransferDescription { get; set; }
        public DateTime? TransferDate { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }

    }
}
