using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialPlanController : ControllerBase
    {
        private readonly IFinancialPlanService _financialPlanService;
        public FinancialPlanController(IFinancialPlanService financialPlanService) 
        {
            _financialPlanService = financialPlanService;
        }

        [HttpGet("GetFinancialPlanLogs")]
        public async Task<dynamic> GetFinancialPlanLogs([FromQuery] int idFinancialPlan)
        {
            return await _financialPlanService.GetFinancialPlanLogs(idFinancialPlan);
        }
    }
}
