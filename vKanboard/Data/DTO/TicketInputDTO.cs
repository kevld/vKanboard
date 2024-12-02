using vKanboard.Data.Models;

namespace vKanboard.Data.DTO
{
    public class TicketInputDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int StatusId { get; set; }
    }
}
