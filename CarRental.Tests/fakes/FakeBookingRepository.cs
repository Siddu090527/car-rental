using CarRental.Api.Entities;
using CarRental.Api.Repositories;

namespace CarRental.Tests.Fakes;

public sealed class FakeBookingRepository : IBookingRepository
{
    private readonly Dictionary<string, Booking> bookings = new();

    public Task<Booking> CreateAsync(
        Booking booking,
        CancellationToken cancellationToken = default)
    {
        bookings[booking.BookingReferenceNumber] = booking;

        return Task.FromResult(booking);
    }

    public Task<Booking?> GetByReferenceAsync(
        string bookingReference,
        CancellationToken cancellationToken = default)
    {
        bookings.TryGetValue(bookingReference, out var booking);

        return Task.FromResult(booking);
    }

    public Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}