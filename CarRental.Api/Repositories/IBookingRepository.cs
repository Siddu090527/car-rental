using CarRental.Api.Entities;

namespace CarRental.Api.Repositories;

public interface IBookingRepository
{
    Task<Booking> CreateAsync(
        Booking booking,
        CancellationToken cancellationToken = default);

    Task<Booking?> GetByReferenceAsync(
        string bookingReference,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}