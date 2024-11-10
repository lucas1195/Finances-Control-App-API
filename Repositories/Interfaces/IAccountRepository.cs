using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using System.Net;

namespace Finances_Control_App_API.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<GetAccountsByUserReturn>> GetAccountsByUser(int userId);
    }
}
