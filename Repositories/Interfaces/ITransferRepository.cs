using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;

namespace Finances_Control_App_API.Repositories.Interfaces
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        Task<IEnumerable<TransferDTO>> GetAllTransactiosByUser(GetAllTransactiosByUserParams parametros);
    }
}
