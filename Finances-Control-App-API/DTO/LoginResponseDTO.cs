namespace Finances_Control_App_API.DTO
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }

        public LoginResponseDTO(bool success, string message, string token = null)
        {
            Success = success;
            Message = message;
            Token = token;
        }
    }
}
