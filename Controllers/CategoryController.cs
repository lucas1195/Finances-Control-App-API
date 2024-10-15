using Finances_Control_App.Domain.FinancesApp;
using Finances_Control_App_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finances_Control_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(Context context) : ControllerBase
    {
        private readonly Context _context = context;


        [HttpGet("GetAll")]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }


        [HttpPost("InsertCategory")]
        public async Task<IActionResult> InsertCategory([FromBody] Category categoryParam)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _context.Category.AddAsync(categoryParam);

                return Ok(await _context.SaveChangesAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromQuery] int CategoryId)
        {
            try
            {
                var retorno = await _context.Category.Where(x => x.CategoryId == CategoryId).FirstOrDefaultAsync();

                if (retorno == null)
                {
                    return BadRequest($"Category with ID {CategoryId} was not found.");
                }

                _context.Category.Remove(retorno);

                return Ok(await _context.SaveChangesAsync());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category categoryParams)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (categoryParams.CategoryId == null)
                {
                    return BadRequest("Category ID cannot be null.");
                }

                await _context.Category.Where(x => x.CategoryId == categoryParams.CategoryId).
                    ExecuteUpdateAsync(x =>
                    x.SetProperty(b => b.CategoryName, categoryParams.CategoryName));

                return Ok(_context.SaveChanges());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
