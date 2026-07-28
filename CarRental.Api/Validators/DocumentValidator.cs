using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Validators;

/// <summary>
/// Validates booking documents based on pickup city.
/// </summary>
public sealed class DocumentValidator : IDocumentValidator
{
    private static readonly HashSet<string> DomesticCities =
    [
        "Hyderabad",
        "Bengaluru"
    ];

    private static readonly HashSet<string> InternationalCities =
    [
        "London",
        "Dubai",
        "Singapore"
    ];

    public bool IsValid(BookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.PickupLocation))
        {
            return false;
        }

        var city = request.PickupLocation.Trim();

        if (DomesticCities.Contains(city))
        {
            return request.DocumentType == DocumentType.NationalId;
        }

        if (InternationalCities.Contains(city))
        {
            return request.DocumentType == DocumentType.Passport;
        }

        return false;
    }
}