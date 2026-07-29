using CarRental.Api.Data;
using CarRental.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Api.Repositories;

/// <summary>
/// Handles all booking-related database operations.
/// This class contains only persistence logic.
/// Business rules remain inside BookingService.
/// </summary>
public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Receives AppDbContext through Dependency Injection.
    /// </summary>
    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Saves a new booking into SQL Server.
    /// </summary>
    public async Task<Booking> CreateAsync(
        Booking booking,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(booking);

        await _context.Bookings.AddAsync(booking, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return booking;
    }

    /// <summary>
    /// Retrieves a booking using its booking reference.
    /// </summary>
    public async Task<Booking?> GetByReferenceAsync(
        string bookingReference,
        CancellationToken cancellationToken = default)
    {
        return await _context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(
                b => b.BookingReferenceNumber == bookingReference,
                cancellationToken);
    }

    /// <summary>
    /// Persists pending Entity Framework changes.
    /// </summary>
    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}