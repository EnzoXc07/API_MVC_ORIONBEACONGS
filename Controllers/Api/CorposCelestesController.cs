using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Dados;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorposCelestesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CorposCelestesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CorpoCeleste>>> GetAll()
        {
            return await _context.CorposCelestes
                .Include(c => c.Areas)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CorpoCeleste>> GetById(int id)
        {
            var corpo = await _context.CorposCelestes
                .Include(c => c.Areas)
                .FirstOrDefaultAsync(c => c.IdCorpo == id);

            if (corpo == null)
                return NotFound();

            return corpo;
        }

        [HttpPost]
        public async Task<ActionResult<CorpoCeleste>> Create(CorpoCeleste corpo)
        {
            _context.CorposCelestes.Add(corpo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = corpo.IdCorpo }, corpo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CorpoCeleste corpo)
        {
            if (id != corpo.IdCorpo)
                return BadRequest();

            _context.Entry(corpo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.CorposCelestes.AnyAsync(c => c.IdCorpo == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var corpo = await _context.CorposCelestes.FindAsync(id);
            if (corpo == null)
                return NotFound();

            _context.CorposCelestes.Remove(corpo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
