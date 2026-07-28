using CarRental.Api.Data;
using CarRental.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Api.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        ArgumentNullException.ThrowIfNull(booking);

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();

        return booking;
    }

    public async Task<Booking?> GetByReferenceAsync(string bookingReference)
    {
        if (string.IsNullOrWhiteSpace(bookingReference))
        {
            return null;
        }

        return await _context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b =>
                b.BookingReferenceNumber == bookingReference);
    }
}