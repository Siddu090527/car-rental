using CarRental.Api.Enums;
using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Validators;

/// <summary>
/// Validates booking documents based on pickup city.
/// </summary>
public sealed class DocumentValidator : IDocumentValidator
{
    private static readonly HashSet<string> DomesticCities;

    private static readonly HashSet<string> InternationalCities;

    static DocumentValidator()
    {
        // Initialize normalized sets using case-insensitive comparer.
        DomesticCities = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            NormalizeCity("Hyderabad"),
            NormalizeCity("Bengaluru")
        };

        InternationalCities = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            NormalizeCity("London"),
            NormalizeCity("Dubai"),
            NormalizeCity("Singapore")
        };
    }

    public bool IsValid(BookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.PickupLocation))
        {
            return false;
        }

        var normalized = NormalizeCity(request.PickupLocation);

        if (DomesticCities.Contains(normalized))
        {
            return request.DocumentType == DocumentType.NationalId;
        }

        if (InternationalCities.Contains(normalized))
        {
            return request.DocumentType == DocumentType.Passport;
        }

        return false;
    }

    private static string NormalizeCity(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Trim and collapse any sequence of whitespace characters into single spaces.
        var parts = input.Trim().Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Join(' ', parts);
    }
}