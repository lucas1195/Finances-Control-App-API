namespace Finances_Control_App_API.Models.DTO
{
    public class GetAllTransactiosByUserParams
    {
        public int? UserId { get; set; }
        public int? AccountId { get; set; }
    }
}
