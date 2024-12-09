using Finances_Control_App_API.DTO;

namespace Finances_Control_App_API.Services.Interfaces
{
    public interface IFinancialPlanService
    {
        Task<List<FinancialPlanLogsResponse>> GetFinancialPlanLogs(int idFinancialPlan);
    }
}
