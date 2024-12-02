using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vKanboard.Data.Models;

namespace vKanboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly Db _db;

        public StatusController(Db db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            return Ok(await _db.Status.Select(x => x.Name).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] Status status)
        {
            _db.Status.Add(status);

            await _db.SaveChangesAsync();

            return Ok(status);
        }
    }
}
