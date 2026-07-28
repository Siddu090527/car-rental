using CarRental.Api.Entities;

namespace CarRental.Api.Repositories;

/// <summary>
/// Defines booking persistence operations.
/// </summary>
public interface IBookingRepository
{
    /// <summary>
    /// Creates and persists a booking.
    /// </summary>
    /// <param name="booking">Booking entity to save.</param>
    /// <returns>The persisted booking.</returns>
    Task<Booking> CreateAsync(Booking booking);

    /// <summary>
    /// Retrieves a booking using the booking reference number.
    /// </summary>
    /// <param name="bookingReference">Unique booking reference.</param>
    /// <returns>The booking if found; otherwise null.</returns>
    Task<Booking?> GetByReferenceAsync(string bookingReference);
}