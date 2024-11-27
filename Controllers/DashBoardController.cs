using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashBoardController(DashBoardService dashBoardService) : ControllerBase
    {
        private readonly DashBoardService _dashboardService = dashBoardService;


        [HttpGet("GetByPeriod")]
        public async Task<IEnumerable<TransferDTO>> GetTransfersByPeriod([FromQuery] GetTransfersByPeriodParams filter)
        {
            return await _dashboardService.GetTransfersByPeriod(filter);
        }

        [HttpGet("GetCategoriesAnalytics")]
        public async Task<IEnumerable<GetCategoriesAnalyticsReturn>> GetCategoriesAnalytics([FromQuery] GetCategoriesAnalyticsParams filter)
        {
            return await _dashboardService.GetCategoriesAnalytics(filter);
        }

        [HttpGet("GetLatest")]
        public async Task<IEnumerable<TransferDTO>> GetLatest([FromQuery] GetLatestParams request)
        {
            return await _dashboardService.GetLatest(request);
        }

        [HttpGet("GetAnalyticsByMonth")]
        public async Task<IEnumerable<GetAnalyticsByMonthReturn>> GetAnalyticsByMonth([FromQuery] int IdUsuario, [FromQuery] int IdConta)
        {
            return await _dashboardService.GetAnalyticsByMonth(IdUsuario, IdConta);
        }
    }
}
