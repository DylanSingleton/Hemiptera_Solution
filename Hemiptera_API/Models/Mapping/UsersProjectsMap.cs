using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hemiptera_API.Models.Mapping;

public class UsersProjectsMap : IEntityTypeConfiguration<UsersProjects>
{
    public void Configure(EntityTypeBuilder<UsersProjects> entityTypeBuilder)
    {
        entityTypeBuilder
            .HasKey(x => new { x.UserId, x.ProjectId });

        entityTypeBuilder
            .HasOne(x => x.User)
            .WithMany(x => x.UsersProjects)
            .HasForeignKey(x => x.UserId);

        entityTypeBuilder
            .HasOne(x => x.Project)
            .WithMany(x => x.UsersProjects)
            .HasForeignKey(x => x.ProjectId);    
    }
}