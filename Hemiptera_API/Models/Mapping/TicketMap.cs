using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hemiptera_API.Models.Mapping;

public class TicketMap : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> entityTypeBuilder)
    {
        // Use fluent API to configure how EF Core maps the Ticket class to the database.
        entityTypeBuilder.HasKey(t => t.Id);
        entityTypeBuilder.Property(x => x.Title).IsRequired();
        entityTypeBuilder.Property(x => x.Summary).IsRequired();
        entityTypeBuilder.Property(x => x.Description).IsRequired();
        entityTypeBuilder.Property(x => x.Priority).IsRequired();
        entityTypeBuilder.Property(x => x.Status).IsRequired();

        entityTypeBuilder
            .HasOne(t => t.Reporter)
            .WithMany()
            .HasForeignKey(t => t.ReporterId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        entityTypeBuilder
            .HasOne(t => t.AssignedTo)
            .WithMany()
            .HasForeignKey(x => x.AssignedToId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        entityTypeBuilder
            .HasOne(t => t.Project)
            .WithMany(t => t.Tickets)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}