using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories;
using Finances_Control_App_API.Repositories.Interfaces;

namespace Finances_Control_App_API.Services
{
    public class FinancialPlanService
    {
        private readonly UserContext _userContext;
        private readonly int _userId;
        private readonly IRepository<FinancialPlan> _financialPlanRepository;
        private readonly IRepository<Transfer> _transferRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<FinancialPlanCategory> _financialPlanCategoryRepository;
        private readonly IRepository<FinancialPlanAccount> _financialAccountRepository;
        private readonly IRepository<AccountFlag> _accountFlagRepository;

        public FinancialPlanService(UserContext userContext, 
            IRepository<Transfer> transferRepository,
            IRepository<User> userRepository,
            IRepository<FinancialPlanCategory> financialPlanCategory, 
            IRepository<FinancialPlanAccount> financialAccountRepository,
            IRepository<FinancialPlan> financialPlanRepository,
            IRepository<Account> accountRepository,
            IRepository<AccountFlag> accountFlagRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _transferRepository = transferRepository;
            _financialPlanRepository = financialPlanRepository;
            _accountRepository = accountRepository;
            _accountFlagRepository = accountFlagRepository;
            _userId = _userContext.GetCurrentUserId();

        }


        public async Task<IEnumerable<FinancialPlanLogsResponse>> GetFinancialPlanLogs(int financialPlanId)
        {
            var query = from t in _transferRepository.Table
                        join u in _userRepository.Table on t.UserId equals u.UserId
                        join fp in _financialPlanRepository.Table on u.UserId equals fp.UserId
                        join a in _accountRepository.Table on t.AccountId equals a.AccountId
                        join af in _accountFlagRepository.Table on a.AccountFlagId equals af.AccountFlagId
                        where t.UserId == _userId && fp.FinancialPlanId == financialPlanId

                        select new FinancialPlanLogsResponse
                        {
                            FanincialPlanId = fp.FinancialPlanId,
                            TransferId = t.TransferId,
                            UserId = _userId,
                            AccountId = t.AccountId,
                            FanincialPlanName = fp.FinancialPlanName,
                            TransferDescription = t.TransferDescription,
                            TransferAmount = t.TransferAmount,
                            TransferDate = t.TransferDate,
                            PlanStartDate = fp.StartDate,
                        };


            return new List<FinancialPlanLogsResponse>();
        }
    }
}
