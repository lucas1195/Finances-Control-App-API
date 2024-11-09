using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models.DTO;
using Finances_Control_App_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController(DashBoardService dashBoardService) : ControllerBase
    {
        private readonly DashBoardService _dashboardService = dashBoardService;


        [HttpGet("GetByPeriod")]
        public async Task<IEnumerable<TransferDTO>> GetByPeriod([FromQuery] GetTransfersByPeriodParams filter)
        {
            return await _dashboardService.GetTransfersByPeriod(filter);
        }

        [HttpGet("GetCategoriesAnalytics")]
        public async Task<IEnumerable<GetCategoriesAnalyticsReturn>> GetCategoriesAnalytics([FromQuery] GetCategoriesAnalyticsParams filter)
        {
            return await _dashboardService.GetCategoriesAnalytics(filter);
        }

        [HttpGet("GetLatest")]
        public async Task<IEnumerable<TransferDTO>> GetLatest([FromQuery] GetLatestParams parametros)
        {
            return await _dashboardService.GetLatest(parametros);
        }

        [HttpGet("GetAnalyticsByMonth")]
        public async Task<IEnumerable<GetAnalyticsByMonthReturn>> GetAnalyticsByMonth([FromQuery] int IdUsuario, [FromQuery] int IdConta)
        {
            return await _dashboardService.GetAnalyticsByMonth(IdUsuario, IdConta);
        }
    }
}
