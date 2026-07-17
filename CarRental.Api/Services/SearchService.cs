using CarRental.Api.Interfaces;
using CarRental.Api.Models;

namespace CarRental.Api.Services;

public sealed class SearchService
{
    private readonly IReadOnlyCollection<ICarRentalProvider> providers;

    public SearchService(IEnumerable<ICarRentalProvider> providers)
    {
        this.providers = providers.ToList();
    }

    public CarSearchResponse Search(CarSearchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var vehicles = providers
            .SelectMany(provider => provider.SearchVehicles(request))
            .ToList();

        return new CarSearchResponse
        {
            Vehicles = vehicles
        };
    }
}
