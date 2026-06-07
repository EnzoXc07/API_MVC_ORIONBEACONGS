using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Dados;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalisesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnalisesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Analise>>> GetAll()
        {
            return await _context.Analises
                .Include(a => a.Area)
                .Include(a => a.Leituras)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Analise>> GetById(int id)
        {
            var analise = await _context.Analises
                .Include(a => a.Area)
                .Include(a => a.Leituras)
                .FirstOrDefaultAsync(a => a.IdAnalise == id);

            if (analise == null)
                return NotFound();

            return analise;
        }

        [HttpGet("por-area/{idArea}")]
        public async Task<ActionResult<IEnumerable<Analise>>> GetByArea(int idArea)
        {
            return await _context.Analises
                .Where(a => a.IdArea == idArea)
                .OrderByDescending(a => a.DataAnalise)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Analise>> Create(Analise analise)
        {
            _context.Analises.Add(analise);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = analise.IdAnalise }, analise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Analise analise)
        {
            if (id != analise.IdAnalise)
                return BadRequest();

            _context.Entry(analise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Analises.AnyAsync(a => a.IdAnalise == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var analise = await _context.Analises.FindAsync(id);
            if (analise == null)
                return NotFound();

            _context.Analises.Remove(analise);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
