using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Services;

/// <summary>
/// Handles vehicle search across all providers.
/// </summary>
public sealed class SearchService
{
    private readonly IReadOnlyCollection<ICarRentalProvider> providers;

    public SearchService(IEnumerable<ICarRentalProvider> providers)
    {
        this.providers = providers.ToList();
    }

    /// <summary>
    /// Searches available vehicles.
    /// </summary>
    public CarSearchResponse Search(CarSearchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var vehicles = providers
            .SelectMany(provider => provider.SearchVehicles(request))
            .ToList();

        // Optional category filter.
        if (request.Category.HasValue)
        {
            vehicles = vehicles
                .Where(v => v.Category == request.Category.Value)
                .ToList();
        }

        // Optional sorting by total price.
        if (request.SortByPrice)
        {
            vehicles = vehicles
                .OrderBy(v => v.DailyRate)
                .ToList();
        }

        return new CarSearchResponse
        {
            Vehicles = vehicles
        };
    }
}