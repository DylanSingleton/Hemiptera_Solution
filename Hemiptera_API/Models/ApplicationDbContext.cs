using Hemiptera_API.Models.Mapping;
using Hemiptera_API.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Hemiptera_API.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        { }

        DbSet<Project> Projects { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(DbContextSettings.ConnectionString);
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ProjectMap(modelBuilder.Entity<Project>());
            new RefreshTokenMap(modelBuilder.Entity<RefreshToken>());
        }
    }
}
