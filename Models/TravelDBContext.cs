using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency;

public partial class TravelDBContext : DbContext
{
    public TravelDBContext()
    {
    }

    public TravelDBContext(DbContextOptions<TravelDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Excursion> Excursions { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<GoodsSpecification> GoodsSpecifications { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<VisitedExcursion> VisitedExcursions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Filename=sql-scripts/travel_db.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Tour).WithMany(p => p.Contracts).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Contracts).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "Tourist",
                    r => r.HasOne<Contract>().WithMany()
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CustomerId", "ContractId");
                        j.ToTable("tourists");
                        j.IndexerProperty<int>("CustomerId").HasColumnName("customer_id");
                        j.IndexerProperty<int>("ContractId").HasColumnName("contract_id");
                    });
        });

        modelBuilder.Entity<Excursion>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Country).WithMany(p => p.Excursions).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DepartureCountry).WithMany(p => p.FlightDepartureCountries).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DestinationCountry).WithMany(p => p.FlightDestinationCountries).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<GoodsSpecification>(entity =>
        {
            entity.HasOne(d => d.Contract).WithMany().OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Flight).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ArrivalFlight).WithMany(p => p.TourArrivalFlights).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Country).WithMany(p => p.Tours).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DepartureFlight).WithMany(p => p.TourDepartureFlights).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Hotel).WithMany(p => p.Tours).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<VisitedExcursion>(entity =>
        {
            entity.HasOne(d => d.Contract).WithMany().OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Excursion).WithMany().OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
