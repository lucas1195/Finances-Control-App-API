namespace Finances_Control_App_API.Models
{
    public class GetTransfersByPeriodParams
    {
        public int IdUsuario { get; set; } = 1;
        public int IdConta { get; set; } = 1;
        public string? FilterType { get; set; } = "Last6Months";
    }
}
