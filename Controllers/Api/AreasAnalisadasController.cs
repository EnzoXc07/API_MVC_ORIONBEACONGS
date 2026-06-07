using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Dados;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasAnalisadasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AreasAnalisadasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaAnalisada>>> GetAll()
        {
            return await _context.AreasAnalisadas
                .Include(a => a.CorpoCeleste)
                .Include(a => a.Missoes)
                .Include(a => a.Analises)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaAnalisada>> GetById(int id)
        {
            var area = await _context.AreasAnalisadas
                .Include(a => a.CorpoCeleste)
                .Include(a => a.Missoes)
                .Include(a => a.Analises)
                .FirstOrDefaultAsync(a => a.IdArea == id);

            if (area == null)
                return NotFound();

            return area;
        }

        [HttpGet("por-corpo/{idCorpo}")]
        public async Task<ActionResult<IEnumerable<AreaAnalisada>>> GetByCorpo(int idCorpo)
        {
            return await _context.AreasAnalisadas
                .Where(a => a.IdCorpo == idCorpo)
                .OrderByDescending(a => a.ScoreRanking)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AreaAnalisada>> Create(AreaAnalisada area)
        {
            _context.AreasAnalisadas.Add(area);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = area.IdArea }, area);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AreaAnalisada area)
        {
            if (id != area.IdArea)
                return BadRequest();

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.AreasAnalisadas.AnyAsync(a => a.IdArea == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var area = await _context.AreasAnalisadas.FindAsync(id);
            if (area == null)
                return NotFound();

            _context.AreasAnalisadas.Remove(area);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
