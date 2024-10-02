using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController(Contexto context) : ControllerBase
    {
        private readonly Contexto _context = context;


        [HttpGet("GetAll")]
        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _context.Categoria.ToListAsync();
        }


        [HttpPost("InserirCategoria")]
        public async Task<IActionResult> InserirCategoria([FromBody] Categoria parametros)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _context.Categoria.AddAsync(parametros);

                return Ok(await _context.SaveChangesAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("ExcluirCategoria")]
        public async Task<IActionResult> ExcluirCategoria([FromQuery] int IdCategoria)
        {
            try
            {
                var retorno = await _context.Categoria.Where(x => x.IdCategoria == IdCategoria).FirstOrDefaultAsync();

                if (retorno == null)
                {
                    return BadRequest("Categoria não encontrada.");
                }

                _context.Categoria.Remove(retorno);

                return Ok(await _context.SaveChangesAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AtualizaCategoria")]
        public async Task<IActionResult> AtualizaCategoria([FromBody] Categoria parametros)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (parametros.IdCategoria == null)
                {
                    return BadRequest("O Id da categoria não pode ser nulo.");
                }

                await _context.Categoria.Where(x => x.IdCategoria == parametros.IdCategoria).
                    ExecuteUpdateAsync(x =>
                    x.SetProperty(b => b.NmCategoria, parametros.NmCategoria));

                return Ok(_context.SaveChanges());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
