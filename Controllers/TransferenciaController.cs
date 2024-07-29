using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferenciaController(Contexto context) : ControllerBase
    {
        private readonly Contexto _context = context;


        [HttpGet("GetAll")]
        public async Task<IQueryable<Transferencia>> GetAll()
        {
            return _context.Transferencia.AsQueryable();
        }

        [HttpPost("AtualizaTransferencia")]
        public async Task<ActionResult> AtualizaTransferencia([FromBody] TransferenciaDTO parametros)
        {
            if (parametros.IdTransferencia == null)
            {
                return BadRequest();
            }

            _context.Transferencia.Where(x => x.IdCliente == editarSituacaoDTO.idCliente &&
                            x.IdCotacao == editarSituacaoDTO.IdCotacao &&
                            x.IdSeguradora == editarSituacaoDTO.IdSeguradora &&
                            x.IdSucursal == editarSituacaoDTO.IdSucursal)
                .ExecuteUpdateAsync(x => x.SetProperty(b => b.Sit
        }



    }
}
