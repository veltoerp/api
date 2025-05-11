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
        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.ToTable("tenants");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.LanguageCode)
                .HasColumnName("language_code");

            entity.Property(e => e.CurrencyCode)
                .HasColumnName("currency_code")
                .HasMaxLength(3);

            entity.Property(e => e.TimeZone)
                .HasColumnName("time_zone")
                .HasDefaultValue("UTC");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("NOW()");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("NOW()");
        });


        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.TenantId)
                .HasColumnName("tenant_id");

            entity.Property(e => e.RoleId)
                .HasColumnName("role_id");

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.Property(e => e.PasswordHash)
                .HasColumnName("password_hash")
                .IsRequired();

            entity.Property(e => e.TimeZone)
                .HasColumnName("time_zone");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("NOW()");


            // Foreign key: tenant_id → tenants.id (ON DELETE CASCADE)
            // entity.HasOne<Tenant>()
            //     .WithMany()
            //     .HasForeignKey(e => e.TenantId)
            //     .OnDelete(DeleteBehavior.Cascade);

            // Foreign key: role_id → roles.id (ON DELETE SET NULL)
            // entity.HasOne<Role>()
            //     .WithMany()
            //     .HasForeignKey(e => e.RoleId)
            //     .OnDelete(DeleteBehavior.SetNull);
            entity.ToTable("users");
        });
        modelBuilder.Entity<Role>(entity =>
       {
            entity.ToTable("roles");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.TenantId)
                .HasColumnName("tenant_id");

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();            
      });

    }
}