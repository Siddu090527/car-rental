using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Services;

/// <summary>
/// Handles vehicle searches across all providers.
/// </summary>
public sealed class SearchService
{
    private readonly IReadOnlyCollection<ICarRentalProvider> _providers;

    public SearchService(IEnumerable<ICarRentalProvider> providers)
    {
        _providers = providers.ToList();
    }

    /// <summary>
    /// Searches vehicles from all providers.
    /// </summary>
    public CarSearchResponse Search(CarSearchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var vehicles = _providers
            .SelectMany(provider => provider.SearchVehicles(request))
            .Where(vehicle => vehicle.IsAvailable)
            .ToList();

        if (request.Category.HasValue)
        {
            vehicles = vehicles
                .Where(vehicle => vehicle.Category == request.Category.Value)
                .ToList();
        }

        if (request.SortByPrice)
        {
            vehicles = vehicles
                .OrderBy(vehicle => vehicle.DailyRate)
                .ToList();
        }

        return new CarSearchResponse
        {
            Vehicles = vehicles
        };
    }
}