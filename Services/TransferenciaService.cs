using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Finances_Control_App_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Transferencia>> GetAll()
        {
            return await _context.Transferencia.ToListAsync();
        }

        public async Task<int> AtualizaTransferencia(Transferencia parametros)
        {
            if (parametros.IdTransferencia == null)
            {
                throw new ArgumentNullException(nameof(parametros.IdTransferencia),"O Id da transferência não pode ser nulo.");
            }

            await _context.Transferencia.Where(x => x.IdTransferencia == parametros.IdTransferencia).
                    ExecuteUpdateAsync(x =>
                    x.SetProperty(b => b.DsTransferencia, parametros.DsTransferencia)
                    .SetProperty(b => b.DtTransferencia, parametros.DtTransferencia)
                    .SetProperty(b => b.VlTransferencia, parametros.VlTransferencia)
                    .SetProperty(b => b.IdCategoria, parametros.IdCategoria));


            return await _context.SaveChangesAsync();
        }

        public async Task<int> ExcluirTransferencia(int IdTransferencia)
        {
            var retorno = await _context.Transferencia.
                Where(x => x.IdTransferencia == IdTransferencia).FirstOrDefaultAsync() ?? throw new ArgumentNullException(nameof(IdTransferencia),"Transferência não encontrada.");

            _context.Transferencia.Remove(retorno);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> InserirTransferencia(Transferencia parametros)
        {

            await _context.Transferencia.AddAsync(parametros);

            return await _context.SaveChangesAsync();
        }
    }
}
