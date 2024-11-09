using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Services
{
    public class TransferenciaService
    {
        private readonly Context _context;
        public TransferenciaService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransferDTO>> GetAllTransactiosByUser(GetAllTransactiosByUserParams parametros)
        {
            var query = $@"SELECT T.TransferId,
                      T.TransferAmount,
                      T.TransferDescription,
                      T.TransferDate,
                      T.CategoryId,
                      T.AccountId,
                      T.UserId
               FROM Transfer T
               WHERE T.UserId = @UserId AND T.AccountId = @AccountId";


            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<TransferDTO>(query, new { parametros.UserId, parametros.AccountId });
        }

        public async Task<IEnumerable<Transfer>> GetAll()
        {
            return await _context.Transfer.ToListAsync();
        }

        public async Task<int> AtualizaTransferencia(Transfer parametros)
        {
            if (parametros.TransferId == null)
            {
                throw new ArgumentNullException(nameof(parametros.TransferId), "The transfer ID cannot be null.");
            }

            await _context.Transfer.Where(x => x.TransferId == parametros.TransferId)
             .ExecuteUpdateAsync(x =>
             x.SetProperty(b => b.TransferDescription, parametros.TransferDescription)
              .SetProperty(b => b.TransferDate, parametros.TransferDate)
              .SetProperty(b => b.TransferAmount, parametros.TransferAmount)
              .SetProperty(b => b.CategoryId, parametros.CategoryId));


            return await _context.SaveChangesAsync();
        }

        public async Task<int> ExcluirTransferencia(int IdTransferencia)
        {
            var retorno = await _context.Transfer.
                Where(x => x.TransferId == IdTransferencia).FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(IdTransferencia), "Transfer not found.");

            _context.Transfer.Remove(retorno);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> InserirTransferencia(Transfer parametros)
        {

            await _context.Transfer.AddAsync(parametros);

            return await _context.SaveChangesAsync();
        }
    }
}
