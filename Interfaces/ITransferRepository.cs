using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.Models.DTO;

namespace Finances_Control_App_API.Interfaces
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        Task<IEnumerable<TransferDTO>> GetAllTransactiosByUser(GetAllTransactiosByUserParams parametros);
    }
}
