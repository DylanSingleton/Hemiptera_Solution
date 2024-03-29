﻿using Hemiptera_API.Models.Mapping;
using Hemiptera_API.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Hemiptera_API.Models;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UsersProjects> UsersProjects { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(DbContextSettings.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProjectMap());
        modelBuilder.ApplyConfiguration(new RefreshTokenMap());
        modelBuilder.ApplyConfiguration(new UsersProjectsMap());
        modelBuilder.ApplyConfiguration(new TicketMap());

    }
}