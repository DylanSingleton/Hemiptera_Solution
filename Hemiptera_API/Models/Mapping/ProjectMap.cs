using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hemiptera_API.Models.Mapping;

public class ProjectMap
{
    public ProjectMap(EntityTypeBuilder<Project> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder.Property(x => x.Name).IsRequired();
        entityTypeBuilder.Property(x => x.Description);
        entityTypeBuilder.Property(x => x.RepositoryLink);
        entityTypeBuilder.Property(x => x.StartDatetTime).IsRequired();
        entityTypeBuilder.Property(x => x.EndDatetTime);
        entityTypeBuilder.Property(x => x.Status).IsRequired();
        entityTypeBuilder.Property(x => x.Type).IsRequired();
    }
}