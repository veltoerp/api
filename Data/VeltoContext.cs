using api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace Velto.Data;
public class VeltoContext : DbContext
{
    public VeltoContext(DbContextOptions<VeltoContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>().ToTable("Tenants");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");

    }
}