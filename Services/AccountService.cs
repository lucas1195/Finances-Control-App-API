using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Finances_Control_App_API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Services
{
    public class AccountService(Contexto context)
    {
        private readonly Contexto _context = context;


        public async Task<int> InsertAccount(Conta account)
        {
            await _context.AddAsync(account);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAccount(Conta account)
        {
            await _context.Conta.Where(x => x.IdConta == account.IdConta).
                ExecuteUpdateAsync(x =>
                x.SetProperty(c => c.NumConta, account.NumConta).
                SetProperty(c => c.NmAgencia, account.NmAgencia).
                SetProperty(c => c.IdAccountFlag, account.IdAccountFlag).
                SetProperty(c => c.Saldo, account.Saldo));

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAccount(int IdConta)
        {
            var conta = _context.Conta.Where(x => x.IdConta == IdConta).FirstOrDefault();

            if (conta == null)
            {
                throw new InvalidOperationException($"Conta de Id {IdConta} não encontrada.");
            }

            _context.Conta.Remove(conta);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetAccountsByUserReturn>> GetAccountsByUser(int IdUsuario)
        {

            var query = $@"SELECT C.IdConta,
                                  C.IdUsuario,
                                  U.NmUsuario,
                                  C.NmAgencia,
                                  C.Saldo,
                                  C.NumConta,
                                  AF.IdAccountFlag,
                                  AF.NmAccountFlag
                           FROM Conta C
                           INNER JOIN Usuario U ON C.IdUsuario = U.IdUsuario
                           LEFT JOIN AccountFlag AF ON C.IdAccountFlag = AF.IdAccountFlag
                           WHERE C.IdUsuario = {IdUsuario}";


            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<GetAccountsByUserReturn>(query, new { IdUsuario });
        }


    }
}
