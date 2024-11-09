using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.Models.DTO;
using System.Net;

namespace Finances_Control_App_API.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<GetAccountsByUserReturn>> GetAccountsByUser(int userId);
    }
}
