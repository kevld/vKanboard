using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vKanboard.Data.DTO;
using vKanboard.Data.Models;

namespace vKanboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly Db _db;

        public TicketController(Db db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketInputDTO ticketDTO)
        {
            var status = await _db.Status.FindAsync(ticketDTO.StatusId);
            _db.Tickets.Add(new Ticket()
            {
                Title = ticketDTO.Title,
                Description = ticketDTO.Description,
                Status = status
            });

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _db.Tickets.Include(x => x.Status)
                .Select(x => new TicketOutputDTO()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Title = x.Title,
                    List = x.Status.Name
                })
                .ToListAsync();


            return Ok(tickets);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketDTO ticketDTO)
        {
            var existing = await _db.Tickets.FindAsync(ticketDTO.Id);
            var newStatus = await _db.Status.FirstOrDefaultAsync(x => x.Name == ticketDTO.NewStatus);
            existing.Status = newStatus;
            await _db.SaveChangesAsync();   

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var existing = await _db.Tickets.FindAsync(id);

            _db.Tickets.Remove(existing);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
