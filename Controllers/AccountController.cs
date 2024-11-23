using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories.Interfaces;
using Finances_Control_App_API.Services;
using Finances_Control_App_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserContext _userContext;

        public AccountController(IAccountRepository accountRepository, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _userContext = userContext;
        }

        [HttpPost("InsertAccount")]
        public async Task<IActionResult> InsertAccount([FromBody] Account account)
        {
            try
            {
                var userId = _userContext.GetCurrentUserId();
                account.UserId = userId;

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
