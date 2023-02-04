using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hemiptera_API.Models.Mapping;

public class TicketMap : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder.Property(x => x.Title).IsRequired();
        entityTypeBuilder.Property(x => x.Summary).IsRequired();
        entityTypeBuilder.Property(x => x.Description).IsRequired();
        entityTypeBuilder.Property(x => x.Priority).IsRequired();
        entityTypeBuilder.Property(x => x.Status).IsRequired();
            
        entityTypeBuilder.HasOne(x => x.Reporter)
            .WithOne()
            .HasForeignKey<Ticket>(x => x.ReporterId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        entityTypeBuilder.HasOne(x => x.AssignedTo)
            .WithOne()
            .HasForeignKey<Ticket>(x => x.AssignedToId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        entityTypeBuilder.HasOne(x => x.Project)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}