using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Finances_Control_App_API.Models.DTO;
using Finances_Control_App_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController : ControllerBase
    {
        private readonly Context _context;
        private readonly TransferenciaService _transferenciaService;

        public TransferenciaController(Context context, TransferenciaService transferenciaService)
        {
            _context = context;
            _transferenciaService = transferenciaService;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Transfer>> GetAll()
        {
            try
            {
                return await _transferenciaService.GetAll();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetAllTranfersByUser")]
        public async Task<IEnumerable<GetAllTransactiosByUserReturn>> GetAllTranfersByUser([FromQuery] GetAllTransactiosByUserParams parametros)
        {
            try
            {
                return await _transferenciaService.GetAllTransactiosByUser(parametros);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("UpdateTransfer")]
        public async Task<IActionResult> UpdateTransfer([FromBody] Transfer parametros)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await _transferenciaService.AtualizaTransferencia(parametros));

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteTransfer")]
        public async Task<IActionResult> DeleteTransfer([FromQuery] int IdTransferencia)
        {
            try
            {

                return Ok(await _transferenciaService.ExcluirTransferencia(IdTransferencia));

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

                return Ok(await _transferenciaService.InserirTransferencia(parametros));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
