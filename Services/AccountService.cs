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


        public async Task<int> InsertAccount(AccountDTO accountDto)
        {
            var account = new Account
            {
                AccountId = accountDto.AccountId,
                UserId = accountDto.UserId,
                AgencyNumber = accountDto.AgencyNumber,
                AccountNumber = accountDto.AccountNumber,
                AccountName = accountDto.AccountName,
                InstitutionName = accountDto.InstitutionName,
                Balance = accountDto.Balance,
                AccountFlagId = accountDto.AccountFlagId,
                TransactionType = accountDto.TransactionType
            };

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

            return await (from a in _context.Account
                          join u in _context.User on a.UserId equals u.UserId
                          join af in _context.AccountFlag on a.AccountFlagId equals af.AccountFlagId into accountFlagJoin
                          from af in accountFlagJoin.DefaultIfEmpty()
                          where a.UserId == UserId
                          select new GetAccountsByUserReturn
                          {
                              AccountId = a.AccountId,
                              UserId = a.UserId,
                              UserName = u.UserName,
                              AgencyNumber = a.AgencyNumber,
                              InstitutionName = a.InstitutionName,
                              Balance = a.Balance,
                              AccountNumber = a.AccountNumber,
                              AccountFlagId = af.AccountFlagId,
                              AccountFlagName = af.AccountFlagName,
                              TransactionType = a.TransactionType

                          }).ToListAsync();
        }


    }
}
