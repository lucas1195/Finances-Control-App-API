namespace Finances_Control_App_API.DTO
{
    public class GetAllTransactiosByUserReturn
    {
        public int? IdTransferencia { get; set; }
        public decimal? VlTransferencia { get; set; }

        public string? DsTransferencia { get; set; }

        public DateTime? DtTransferencia { get; set; }

        public int? IdCategoria { get; set; }

        public string? NmCategoria { get; set; }

        public int IdConta { get; set; }
        public int IdUsuario { get; set; }
    }
}
