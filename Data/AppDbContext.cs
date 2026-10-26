using System;
using Microsoft.EntityFrameworkCore;
using system_zgloszen_ur.Entities;

namespace system_zgloszen_ur.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Failure> Failures { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Repair> Repairs { get; set; } = null!;
    
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>(e =>
    {
        e.HasMany(u => u.Failures)
            .WithOne(f => f.ReportedBy)
            .HasForeignKey(f => f.ReportedById)
            .OnDelete(DeleteBehavior.Cascade);


        e.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
        e.Property(u => u.Email).IsRequired().HasMaxLength(255);
        e.HasIndex(u => u.Email).IsUnique();
        e.Property(u => u.Name).IsRequired().HasMaxLength(255);
        e.Property(u => u.Role).IsRequired().HasConversion<string>().HasMaxLength(50);
    });

    modelBuilder.Entity<Failure>(e =>
    {
        e.HasMany(f => f.Repairs)
            .WithOne(r => r.Failure)
            .HasForeignKey(r => r.FailureId)
            .OnDelete(DeleteBehavior.Cascade);

        e.Property(f => f.Title).IsRequired().HasMaxLength(255);
        e.Property(f => f.FailureDescription).IsRequired().HasMaxLength(1000);
        e.Property(f => f.MachineName).IsRequired().HasMaxLength(255);
        e.Property(f => f.Location).IsRequired().HasMaxLength(255);
        e.Property(f => f.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
        e.HasIndex(f => f.Status);
        
    });

    modelBuilder.Entity<Repair>(e =>
    {
        e.HasOne(r => r.PerformedBy)
            .WithMany(u => u.Repairs)
            .HasForeignKey(r => r.PerformedById)
            .OnDelete(DeleteBehavior.Restrict);
            
        e.Property(r => r.RepairDescription).HasMaxLength(1000);
        
    });
        
}

}

