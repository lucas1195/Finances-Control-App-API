namespace Finances_Control_App_API.DTO
{
    public class GetAllTransactiosByUserParams
    {
        public int? UserId { get; set; }
        public int? AccountId { get; set; }
    }
}
