using Finances_Control_App_API.DTO;

namespace Finances_Control_App_API.Services.Interfaces
{
    public interface IFinancialPlanService
    {
        Task<dynamic> GetFinancialPlanLogs(int financialPlanId);

        Task<dynamic> GetWeeklyFinancialPlanLogs(int financialPlanId);
    }
}
