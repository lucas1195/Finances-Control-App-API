namespace Finances_Control_App_API.Models
{
    public class GetAnalyticsByMonthReturn
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
