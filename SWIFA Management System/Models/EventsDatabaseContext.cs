using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SWIFA_Management_System.Models;

public partial class EventsDatabaseContext : DbContext
{
    public EventsDatabaseContext()
    {
    }

    public EventsDatabaseContext(DbContextOptions<EventsDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    public virtual DbSet<School> Schools { get; set; }
    public virtual DbSet<Pool> Pools { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Set up configuration to read from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get the connection string by name
            string connectionString = config.GetConnectionString("EventsDatabase");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events__3214EC077AFE1625");

            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(100);
        });

        modelBuilder.Entity<Team>(entity => {
            entity.HasKey(e => e.TeamId).HasName("PK__Teams__3214EC077AFE1625");
            entity.Property(e => e.School).HasMaxLength(100);
            entity.Property(e => e.suffix).HasMaxLength(100);
            entity.Property(e => e.Blade).HasMaxLength(100);
            entity.Property(e => e.AFencer).HasMaxLength(100);
            entity.Property(e => e.BFencer).HasMaxLength(100);
            entity.Property(e => e.CFencer).HasMaxLength(100);
            entity.Property(e => e.AltFencer).HasMaxLength(100);
            entity.Property(e => e.EventId).IsRequired();
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schools__3214EC077AFE1625");
            entity.Property(e => e.SchoolName).HasMaxLength(100);
        });
        modelBuilder.Entity<Pool>(entity=>
        {
            entity.HasKey(e => e.PoolId).HasName("PK__Pools__3214EC077AFE1625");
            entity.Property(e => e.Blade).HasMaxLength(100);
            entity.Property(e => e.PoolNum).IsRequired();
            entity.Property(e => e.EventId).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
