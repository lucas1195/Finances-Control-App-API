using Finances_Control_App_API.DTO;

namespace Finances_Control_App_API.Services.Interfaces
{
    public interface IDashBoardService
    {
        Task<IEnumerable<TransferDTO>> GetTransfersByPeriod(GetTransfersByPeriodParams filter);

        Task<IEnumerable<GetCategoriesAnalyticsReturn>> GetCategoriesAnalytics(GetCategoriesAnalyticsParams filter);

        Task<IEnumerable<TransferDTO>> GetLatest(GetLatestParams request);

        Task<IEnumerable<GetAnalyticsByMonthReturn>> GetAnalyticsByMonth(int idConta);
    }
}
