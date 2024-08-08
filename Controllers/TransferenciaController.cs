using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<dynamic> AtualizaTransferencia([FromBody] TransferenciaDTO parametros)
        {
            if (parametros.IdTransferencia == null)
            {
                return BadRequest();
            }

            return _context.Transferencia.Where(x => x.IdTransferencia == parametros.IdTransferencia).
                ExecuteUpdateAsync(x =>
                x.SetProperty(b => b.DsTransferencia, parametros.DsTransferencia)
                .SetProperty(b => b.DtTransferencia, parametros.DtTransferencia)
                .SetProperty(b => b.VlTransferencia, parametros.VlTransferencia)
                .SetProperty(b => b.IdCategoria, parametros.IdCategoria));

        }



    }
}
