using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;

namespace Finances_Control_App_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDTO> LogginUser(User loginRequestModel);

        Task UpdatePassword(User user, string newPassword);
    }
}
