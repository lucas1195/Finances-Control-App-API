namespace Finances_Control_App_API.Models
{
    public class GetAccountsByUserReturn
    {
        public int IdConta { get; set; }
        public int IdUsuario { get; set; }
        public string NmUsuario { get; set; }
        public string NmAgencia { get; set; }
        public int NumConta { get; set; }
        public double Saldo { get; set; }
        public int? IdAccontFlag { get; set; }
        public string? NmAccountFlag { get; set; }
    }
}
