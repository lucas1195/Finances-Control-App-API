using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
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
            return await _context.Transferencia.ToListAsync();
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
                if (parametros.IdTransferencia == null)
                {
                    return BadRequest("O Id da transferência não pode ser nulo.");
                }

                await _context.Transferencia.Where(x => x.IdTransferencia == parametros.IdTransferencia).
                    ExecuteUpdateAsync(x =>
                    x.SetProperty(b => b.DsTransferencia, parametros.DsTransferencia)
                    .SetProperty(b => b.DtTransferencia, parametros.DtTransferencia)
                    .SetProperty(b => b.VlTransferencia, parametros.VlTransferencia)
                    .SetProperty(b => b.IdCategoria, parametros.IdCategoria));

                return Ok(_context.SaveChanges());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("ExcluirTransferencia")]
        public async Task<IActionResult> ExcluirTransferencia([FromQuery] int IdTransferencia)
        {
            try
            {
                var retorno = await _context.Transferencia.Where(x => x.IdTransferencia == IdTransferencia).FirstOrDefaultAsync();

                if (retorno == null)
                {
                    return BadRequest("Transferência não encontrada.");
                }

                _context.Transferencia.Remove(retorno);

                return Ok(await _context.SaveChangesAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InserirTransferencia")]
        public async Task<IActionResult> InserirTransferencia([FromBody] Transferencia parametros)
        {
            try
            {
                _context.Transferencia.Add(parametros);

                return Ok(await _context.SaveChangesAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
