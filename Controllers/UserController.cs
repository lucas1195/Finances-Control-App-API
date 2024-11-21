using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;


        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Add(LoginRequestModel loginRequestModel)
        {
            var result = await _userService.LogginUser(loginRequestModel);

            if (result.Success)
            {
                Response.Headers.Append("Authorization", "Bearer " + result.Token);
                return Ok(result);
            }
            else
            {
                return BadRequest(new { message = "Invalid email or password." });
            }
        }

        [HttpPost("UpdatePassword")]
        public async Task UpdatePassword(User loginRequestModel, string newPassword)
        {
           await _userService.UpdatePassword(loginRequestModel, newPassword);
        }

    }
}
