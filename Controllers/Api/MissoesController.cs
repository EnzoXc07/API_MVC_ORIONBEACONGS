using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Dados;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MissoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Missao>>> GetAll()
        {
            return await _context.Missoes
                .Include(m => m.Area)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Missao>> GetById(int id)
        {
            var missao = await _context.Missoes
                .Include(m => m.Area)
                .FirstOrDefaultAsync(m => m.IdMissao == id);

            if (missao == null)
                return NotFound();

            return missao;
        }

        [HttpGet("total")]
        public async Task<ActionResult<int>> GetTotal()
        {
            return await _context.Missoes.CountAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Missao>> Create(Missao missao)
        {
            _context.Missoes.Add(missao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = missao.IdMissao }, missao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Missao missao)
        {
            if (id != missao.IdMissao)
                return BadRequest();

            _context.Entry(missao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Missoes.AnyAsync(m => m.IdMissao == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var missao = await _context.Missoes.FindAsync(id);
            if (missao == null)
                return NotFound();

            _context.Missoes.Remove(missao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
