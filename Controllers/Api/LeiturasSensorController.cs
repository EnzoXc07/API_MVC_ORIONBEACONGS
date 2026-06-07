using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Dados;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeiturasSensorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeiturasSensorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeituraSensor>>> GetAll()
        {
            return await _context.LeiturasSensor
                .Include(l => l.Analise)
                .Include(l => l.Sensor)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeituraSensor>> GetById(int id)
        {
            var leitura = await _context.LeiturasSensor
                .Include(l => l.Analise)
                .Include(l => l.Sensor)
                .FirstOrDefaultAsync(l => l.IdLeitura == id);

            if (leitura == null)
                return NotFound();

            return leitura;
        }

        [HttpGet("por-analise/{idAnalise}")]
        public async Task<ActionResult<IEnumerable<LeituraSensor>>> GetByAnalise(int idAnalise)
        {
            return await _context.LeiturasSensor
                .Where(l => l.IdAnalise == idAnalise)
                .Include(l => l.Sensor)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<LeituraSensor>> Create(LeituraSensor leitura)
        {
            _context.LeiturasSensor.Add(leitura);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = leitura.IdLeitura }, leitura);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LeituraSensor leitura)
        {
            if (id != leitura.IdLeitura)
                return BadRequest();

            _context.Entry(leitura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.LeiturasSensor.AnyAsync(l => l.IdLeitura == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var leitura = await _context.LeiturasSensor.FindAsync(id);
            if (leitura == null)
                return NotFound();

            _context.LeiturasSensor.Remove(leitura);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
