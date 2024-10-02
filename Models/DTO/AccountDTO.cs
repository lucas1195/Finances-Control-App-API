namespace Finances_Control_App_API.Models.DTO
{
    public class AccountDTO
    {
        public int IdConta { get; set; }
        public int IdUsuario { get; set; }

        public string NmAgencia { get; set; }

        public string NumConta { get; set; }

        public int Saldo { get; set; }

        public int? IdAccountFlag { get; set; }
    }
}
