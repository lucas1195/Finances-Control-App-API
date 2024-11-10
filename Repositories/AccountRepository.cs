using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<AccountFlag> _accountFlagRepository;

        public AccountRepository(Context context, IRepository<AccountFlag> accountFlagRepository, IRepository<User> userRepository) : base(context)
        {
            _accountFlagRepository = accountFlagRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetAccountsByUserReturn>> GetAccountsByUser(int userId)
        {

            return await (from a in Table
                          join u in _userRepository.Table on a.UserId equals u.UserId
                          join af in _accountFlagRepository.Table on a.AccountFlagId equals af.AccountFlagId into accountFlagJoin
                          from af in accountFlagJoin.DefaultIfEmpty()
                          where a.UserId == userId
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
