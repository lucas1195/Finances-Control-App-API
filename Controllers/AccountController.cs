using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.Infraestructure.Repositories;
using Finances_Control_App_API.Interfaces;
using Finances_Control_App_API.Models.DTO;
using Finances_Control_App_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("InsertAccount")]
        public async Task<IActionResult> InsertAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _accountRepository.AddAsync(account));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("UpdateAccount")]
        public async Task UpdateAccount([FromBody] Account account)
        {
            await _accountRepository.UpdateAsync(account);
        }

        [HttpDelete("DeleteAccount")]
        public async Task DeleteAccount([FromQuery] int idConta)
        {
            await _accountRepository.DeleteAsync(idConta);
        }

        [HttpGet("GetAccountsByUser")]
        public async Task<IEnumerable<GetAccountsByUserReturn>> GetAccountsByUser([FromQuery] int userId)
        {
            try
            {
                return await _accountRepository.GetAccountsByUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
