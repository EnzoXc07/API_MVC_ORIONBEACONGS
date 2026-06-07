using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Dados;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class NosqlAreasJsonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NosqlAreasJsonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NosqlAreaJson>>> GetAll()
        {
            return await _context.NosqlAreasJson.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NosqlAreaJson>> GetById(int id)
        {
            var documento = await _context.NosqlAreasJson.FindAsync(id);
            if (documento == null)
                return NotFound();

            return documento;
        }

        [HttpPost]
        public async Task<ActionResult<NosqlAreaJson>> Create(NosqlAreaJson documento)
        {
            _context.NosqlAreasJson.Add(documento);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = documento.Id }, documento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NosqlAreaJson documento)
        {
            if (id != documento.Id)
                return BadRequest();

            _context.Entry(documento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.NosqlAreasJson.AnyAsync(n => n.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var documento = await _context.NosqlAreasJson.FindAsync(id);
            if (documento == null)
                return NotFound();

            _context.NosqlAreasJson.Remove(documento);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
