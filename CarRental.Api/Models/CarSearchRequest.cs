using CarRental.Api.Enums;

namespace CarRental.Api.Models;

/// <summary>
/// Represents the search criteria for available rental vehicles.
/// </summary>
public sealed class CarSearchRequest
{
    /// <summary>
    /// Pickup city.
    /// Example: Hyderabad, Bengaluru, London.
    /// </summary>
    public string PickupLocation { get; set; } = string.Empty;

    /// <summary>
    /// Rental start date.
    /// </summary>
    public DateTime PickupDate { get; set; }

    /// <summary>
    /// Rental end date.
    /// </summary>
    public DateTime ReturnDate { get; set; }

    /// <summary>
    /// Optional vehicle category filter.
    /// </summary>
    public VehicleCategory? Category { get; set; }

    /// <summary>
    /// Optional sort by total rental price.
    /// False = provider/default order.
    /// True = ascending total price.
    /// </summary>
    public bool SortByPrice { get; set; }
}