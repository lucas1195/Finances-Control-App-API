using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Services
{
    public class TransferenciaService
    {
        private readonly Contexto _context;
        public TransferenciaService(Contexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetAllTransactiosByUserReturn>> GetAllTransactiosByUser(GetAllTransactiosByUserParams parametros)
        {
            var query = $@"SELECT T.IdTransferencia,
                                  T.VlTransferencia,
                                  T.DsTransferencia,
                                  T.DtTransferencia,
                                  T.IdCategoria,
                                  C.NmCategoria,
                                  T.IdConta,
                                  T.IdUsuario
                           FROM Transferencia T
                           JOIN Categoria C ON T.IdCategoria = C.IdCategoria
                           WHERE T.IdUsuario = {parametros.IdUsuario} AND T.IdConta = {parametros.IdConta}";

            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<GetAllTransactiosByUserReturn>(query, new { parametros.IdUsuario, parametros.IdConta });
        }
    }
}
