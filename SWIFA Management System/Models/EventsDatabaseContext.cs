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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
