using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.Interfaces;
using Finances_Control_App_API.Models.DTO;
using Finances_Control_App_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Infraestructure.Repositories
{
    public class TransferRepository : Repository<Transfer>, ITransferRepository
    {

        public TransferRepository(Context context) : base(context) { }
        public async Task<IEnumerable<TransferDTO>> GetAllTransactiosByUser(GetAllTransactiosByUserParams parametros)
        {
            return await Table
            .Where(t => t.UserId == parametros.UserId && t.AccountId == parametros.AccountId)
            .Select(t => new TransferDTO
            {
                TransferId = t.TransferId,
                TransferAmount = t.TransferAmount,
                TransferDescription = t.TransferDescription,
                TransferDate = t.TransferDate,
                CategoryId = t.CategoryId,
                AccountId = t.AccountId,
                UserId = t.UserId
            }).ToListAsync();

        }
    }
}
