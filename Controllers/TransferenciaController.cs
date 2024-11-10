using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App.Domain.FinancesApp.Models;
using Finances_Control_App_API.DTO;
using Finances_Control_App_API.Repositories;
using Finances_Control_App_API.Repositories.Interfaces;
using Finances_Control_App_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController(ITransferRepository transferRepository) : ControllerBase
    {
        private readonly ITransferRepository _transferRepository = transferRepository;

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Transfer>> GetAll()
        {
            return await _transferRepository.GetAllAsync();
        }

        [HttpGet("GetAllTranfersByUser")]
        public async Task<IEnumerable<TransferDTO>> GetAllTranfersByUser([FromQuery] GetAllTransactiosByUserParams parametros)
        {
            return await _transferRepository.GetAllTransactiosByUser(parametros);
        }

        [HttpPost("UpdateTransfer")]
        public async Task UpdateTransfer([FromBody] Transfer parametros)
        {
            await _transferRepository.UpdateAsync(parametros);
        }

        [HttpDelete("DeleteTransfer")]
        public async Task DeleteTransfer([FromQuery] int transferId)
        {
            await _transferRepository.DeleteAsync(transferId);
        }

        [HttpPost("InsertTransfer")]
        public async Task<IActionResult> InsertTransfer([FromBody] Transfer parametros)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _transferRepository.AddAsync(parametros));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
