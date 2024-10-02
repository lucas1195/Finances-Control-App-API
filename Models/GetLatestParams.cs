namespace Finances_Control_App_API.Models
{
    public class GetLatestParams
    {
        public int IdUsuario { get; set; }

        public int IdConta { get; set; }

        public int Top { get; set; } = 10;
    }
}
