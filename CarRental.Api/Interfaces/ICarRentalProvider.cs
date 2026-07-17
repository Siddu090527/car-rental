using CarRental.Api.Enums;
using CarRental.Api.Models;

namespace CarRental.Api.Interfaces;

public interface ICarRentalProvider
{
    ProviderType ProviderType { get; }

    IEnumerable<ProviderVehicle> SearchVehicles(CarSearchRequest request);

    BookingResponse Book(BookingRequest request, PriceBreakdown priceBreakdown);
}
