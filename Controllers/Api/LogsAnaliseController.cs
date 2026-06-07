using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj_OrionBeacon.Dados;
using Proj_OrionBeacon.Models;

namespace Proj_OrionBeacon.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsAnaliseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogsAnaliseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogAnalise>>> GetAll()
        {
            return await _context.LogsAnalise
                .OrderByDescending(l => l.DataLog)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogAnalise>> GetById(int id)
        {
            var log = await _context.LogsAnalise.FindAsync(id);
            if (log == null)
                return NotFound();

            return log;
        }
    }
}
