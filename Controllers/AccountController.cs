using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Finances_Control_App_API.Models.DTO;
using Finances_Control_App_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AccountService accountService) : ControllerBase
    {
        private readonly AccountService _accountService = accountService;

        [HttpPost("InsertAccount")]
        public async Task<IActionResult> InsertAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _accountService.InsertAccount(account));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _accountService.UpdateAccount(account));
                
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromQuery] int IdConta)
        {
            try
            {
                return Ok(await _accountService.DeleteAccount(IdConta));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAccountsByUser")]
        public async Task<IEnumerable<GetAccountsByUserReturn>> GetAccountsByUser([FromQuery] int IdUsuario)
        {
            try
            {
                return await _accountService.GetAccountsByUser(IdUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
    }
}
