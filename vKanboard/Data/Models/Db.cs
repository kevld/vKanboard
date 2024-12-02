using Microsoft.EntityFrameworkCore;

namespace vKanboard.Data.Models
{
    public class Db : DbContext
    {
        public DbSet<Status> Status => Set<Status>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public Db(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TicketEntityTypeConfiguration());
        }
    }
}
