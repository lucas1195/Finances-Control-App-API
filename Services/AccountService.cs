using Dapper;
using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Finances_Control_App_API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Finances_Control_App_API.Services
{
    public class AccountService(Context context)
    {
        private readonly Context _context = context;


        public async Task<int> InsertAccount(Account account)
        {
            await _context.AddAsync(account);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAccount(Account account)
        {
            await _context.Account.Where(x => x.AccountId == account.AccountId).
                 ExecuteUpdateAsync(x =>
                 x.SetProperty(c => c.AccountNumber, account.AccountNumber).
                 SetProperty(c => c.AgencyNumber, account.AgencyNumber).
                 SetProperty(c => c.AccountName, account.AccountName).
                 SetProperty(c => c.InstitutionName, account.InstitutionName).
                 SetProperty(c => c.AccountFlagId, account.AccountFlagId).
                 SetProperty(c => c.Balance, account.Balance));             

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAccount(int accountId)
        {
            var conta = _context.Account.Where(x => x.AccountId == accountId).FirstOrDefault();

            if (conta == null)
            {
                throw new InvalidOperationException($"Account with Id {accountId} not found.");
            }

            _context.Account.Remove(conta);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetAccountsByUserReturn>> GetAccountsByUser(int UserId)
        {

            var query = $@"SELECT C.AccountId,
                          C.UserId,
                          U.UserName,
                          C.AgencyNumber,
                          C.Balance,
                          C.AccountNumber,
                          AF.AccountFlagId,
                          AF.AccountFlagName
                   FROM Account C
                   INNER JOIN User U ON C.UserId = U.UserId
                   LEFT JOIN AccountFlag AF ON C.AccountFlagId = AF.AccountFlagId
                   WHERE C.UserId = @UserId";


            using var connection = _context.Database.GetDbConnection();

            return await connection.QueryAsync<GetAccountsByUserReturn>(query, new { UserId });
        }


    }
}
