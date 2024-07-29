namespace Finances_Control_App_API.Models
{
    public class TransferenciaDTO
    {
        public int? IdTransferencia { get; set; }
        public decimal? VlTransferencia { get; set; }

        public string? DsTransferencia { get; set; }

        public DateTime? DtTransferencia { get; set; }

        public int? IdCategoria { get; set; }
    }
}
