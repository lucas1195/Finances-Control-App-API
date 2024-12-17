using Finances_Control_App.Domain.FinancesApp.Enums;
using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories;
using Finances_Control_App_API.Repositories.Interfaces;
using Finances_Control_App_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Services
{
    public class FinancialPlanService : IFinancialPlanService
    {
        private readonly IUserContext _userContext;
        private readonly int _userId;
        private readonly IRepository<FinancialPlan> _financialPlanRepository;
        private readonly IRepository<Transfer> _transferRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<FinancialPlanCategory> _financialPlanCategoryRepository;
        private readonly IRepository<FinancialPlanAccount> _financialPlanAccountRepository;
        private readonly IRepository<Category> _categoryRepository;


        public FinancialPlanService(IUserContext userContext, 
            IRepository<Transfer> transferRepository,
            IRepository<User> userRepository,
            IRepository<FinancialPlanCategory> financialPlanCategoryRepository, 
            IRepository<FinancialPlanAccount> financialPlanAccountRepository,
            IRepository<FinancialPlan> financialPlanRepository,
            IRepository<Account> accountRepository,
            IRepository<Category> categoryRepository)
        {
            _userContext = userContext;
            _userRepository = userRepository;
            _transferRepository = transferRepository;
            _financialPlanRepository = financialPlanRepository;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _financialPlanCategoryRepository = financialPlanCategoryRepository;
            _financialPlanAccountRepository = financialPlanAccountRepository;
            _userId = _userContext.GetCurrentUserId();

        }


        public async Task<dynamic> GetFinancialPlanLogs(int financialPlanId)
        {
            var listCategoryPriorityIds = _financialPlanCategoryRepository.Table
                .Where(x => x.FinancialPlanId == financialPlanId && x.PlanCategoryType == FinancialPlanCategoryType.Priority)
                .Select(x => x.CategoryId)
                .ToList();

            var financialPlanAccoutIdList = _financialPlanAccountRepository.Table
                .Where(x => x.UserId == _userId && x.FinancialPlanId == financialPlanId)
                .Select(x => x.AccountId)
                .ToList();

            var today = DateTime.Now;
            var daysUntilNextSunday = ((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7) % 7;
            var nextSunday = today.AddDays(daysUntilNextSunday);
            var daysUntilThisSunday = DayOfWeek.Sunday - today.DayOfWeek;
            var thisWeekSunday = today.AddDays(daysUntilThisSunday);

            var query = (from t in _transferRepository.Table
                         join u in _userRepository.Table on t.UserId equals u.UserId
                         join fp in _financialPlanRepository.Table on u.UserId equals fp.UserId
                         join a in _accountRepository.Table on t.AccountId equals a.AccountId
                         join c in _categoryRepository.Table on t.CategoryId equals c.CategoryId
                         where t.UserId == _userId && 
                         fp.FinancialPlanId == financialPlanId && 
                         t.TransferDate >= fp.StartDate && 
                         t.TransferDate < nextSunday && 
                         financialPlanAccoutIdList.Contains(a.AccountId)
                         
                         select new FinancialPlanLogsResponse
                         {
                             FinancialPlanId = fp.FinancialPlanId,
                             TransferId = t.TransferId,
                             UserId = _userId,
                             AccountId = t.AccountId,
                             FinancialPlanName = fp.FinancialPlanName,
                             TransferDescription = t.TransferDescription,
                             TransferAmount = t.TransferAmount,
                             TransferDate = t.TransferDate,
                             PlanStartDate = fp.StartDate,
                             AccountFlagId = a.AccountFlagId,
                             CategoryId = c.CategoryId,
                             CategoryName = c.CategoryName,
                             CategoryType = listCategoryPriorityIds.Contains(t.CategoryId) ? FinancialPlanCategoryType.Priority : FinancialPlanCategoryType.Personal,
                             DayOfWeek = (int)t.TransferDate.DayOfWeek,

                         }).AsEnumerable()
                         .GroupBy(x => x.DayOfWeek)
                         .Select(g => new
                         {
                             DayOfWeek = g.Key,
                             TransfersCount = g.Count(),
                             TransfersAmount = g.Sum(x => x.TransferAmount),
                             PrioritySpent = g.Where(x => listCategoryPriorityIds.Contains(x.CategoryId)).Sum(x => x.TransferAmount),
                             PersonalSpent = g.Where(x => !listCategoryPriorityIds.Contains(x.CategoryId)).Sum(x => x.TransferAmount),
                             Transfers = g.ToList()
                         })
                         .ToList();
                         
            
            return query;
        }

        public async Task<dynamic> GetWeeklyFinancialPlanLogs(int financialPlanId)
        {

            var listCategoryPriorityIds = _financialPlanCategoryRepository.Table
                 .Where(x => x.FinancialPlanId == financialPlanId && x.PlanCategoryType == FinancialPlanCategoryType.Priority)
                 .Select(x => x.CategoryId)
                 .ToList();

            var financialPlanAccoutIdList = _financialPlanAccountRepository.Table
                .Where(x => x.UserId == _userId && x.FinancialPlanId == financialPlanId)
                .Select(x => x.AccountId)
                .ToList();


            var financialPlan = _financialPlanRepository.Table
                .Where(x => x.FinancialPlanId == financialPlanId)
                .FirstOrDefault();

            if (financialPlan == null) {

                throw new Exception($"The Financial Plan with ID: {financialPlanId} was not found.");
            }

            var query = (from t in _transferRepository.Table
                        join a in _accountRepository.Table on t.AccountId equals a.AccountId
                        where t.TransferDate >= financialPlan.StartDate && financialPlanAccoutIdList.Contains(a.AccountId)
                        select new
                        {
                            Day = t.TransferDate.Date,
                            t.TransferAmount,
                            t.CategoryId

                        }).AsEnumerable()
                        .GroupBy(x => x.Day.Date)
                        .Select(g => new
                        {
                            Date = g.Key,
                            PrioritySpent = g.Where(x => listCategoryPriorityIds.Contains(x.CategoryId)).Sum(x => x.TransferAmount),
                            PersonalSpent = g.Where(x => !listCategoryPriorityIds.Contains(x.CategoryId)).Sum(x => x.TransferAmount)
                        })
                        .ToList();
            
            
            var groupedByWeek = query.GroupBy(x => x.Date.AddDays(-(int)x.Date.DayOfWeek))
                .Select(g => new
                {
                    WeekStartDate = g.Key,
                    DailyData = g.Select(d => new
                    {
                        Date = d.Date,
                        PrioritySpent = d.PrioritySpent,
                        PersonalSpent = d.PersonalSpent
                    }).OrderBy(x => x.Date).ToList()
                })
                .ToList();

            return groupedByWeek;
        }
    }
}
