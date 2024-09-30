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
        private readonly Contexto _context;
        private readonly TransferenciaService _transferenciaService;

        public TransferenciaController(Contexto context, TransferenciaService transferenciaService)
        {
            _context = context;
            _transferenciaService = transferenciaService;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Transferencia>> GetAll()
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

        [HttpGet("GetAllTransactiosByUser")]
        public async Task<IEnumerable<GetAllTransactiosByUserReturn>> GetAllTransactiosByUser([FromQuery] GetAllTransactiosByUserParams parametros)
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

        [HttpPost("AtualizaTransferencia")]
        public async Task<IActionResult> AtualizaTransferencia([FromBody] TransferenciaDTO parametros)
        {
            try
            {

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

        [HttpDelete("ExcluirTransferencia")]
        public async Task<IActionResult> ExcluirTransferencia([FromQuery] int IdTransferencia)
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

        [HttpPost("InserirTransferencia")]
        public async Task<IActionResult> InserirTransferencia([FromBody] TransferenciaDTO parametros)
        {
            try
            {

                return Ok(await _transferenciaService.InserirTransferencia(parametros));

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

    }
}
