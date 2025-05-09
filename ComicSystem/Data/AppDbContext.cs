using ComicSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ComicBook> ComicBooks { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalDetail> RentalDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customer → Rentals (1:N)
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Rentals)
            .WithOne(r => r.Customer)
            .HasForeignKey(r => r.CustomerId);

        // Rental → RentalDetails (1:N)
        modelBuilder.Entity<Rental>()
            .HasMany(r => r.RentalDetails)
            .WithOne(rd => rd.Rental)
            .HasForeignKey(rd => rd.RentalId);

        // ComicBook → RentalDetails (1:N)
        modelBuilder.Entity<ComicBook>()
            .HasMany(cb => cb.RentalDetails)
            .WithOne(rd => rd.ComicBook)
            .HasForeignKey(rd => rd.ComicBookId);

        // Composite primary key for RentalDetail
        modelBuilder.Entity<RentalDetail>()
            .HasKey(rd => new { rd.RentalId, rd.ComicBookId });
        
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.PhoneNumber)
            .IsUnique();

    }

}