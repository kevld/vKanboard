using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace vKanboard.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }
        public int StatusId { get; set; }
    }

    public class TicketEntityTypeConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
            builder.HasOne(e => e.Status)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.StatusId)
                .IsRequired();
        }
    }
}
