using Hemiptera_API.Models.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hemiptera_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        DbSet<Project> Projects { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ProjectMap(modelBuilder.Entity<Project>());
        }
    }
}
