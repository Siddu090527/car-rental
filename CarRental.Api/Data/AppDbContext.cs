using CarRental.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Api.Data;

// This class acts as the bridge between our .NET application and SQL Server.
// Entity Framework uses it to map our C# entities to database tables.
public class AppDbContext : DbContext
{
    // Dependency Injection provides the database configuration.
    // This keeps the DbContext reusable across Development, QA, and Production.
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // This DbSet becomes the Bookings table in SQL Server.
    public DbSet<Booking> Bookings { get; set; } = null!;

    // Configure database-specific settings here instead of scattering them across the application.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TotalPrice represents a currency value.
        // I'm explicitly setting the precision to avoid SQL Server choosing a default
        // that could round or truncate values in production.
        modelBuilder.Entity<Booking>()
            .Property(b => b.TotalPrice)
            .HasPrecision(18, 2);
    }
}