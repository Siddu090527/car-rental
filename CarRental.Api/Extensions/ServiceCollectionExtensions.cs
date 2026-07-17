using CarRental.Api.Interfaces;
using CarRental.Api.Pricing;
using CarRental.Api.Providers;
using CarRental.Api.Services;
using CarRental.Api.Validators;

namespace CarRental.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCarRentalServices(this IServiceCollection services)
    {
        services.AddSingleton<ICarRentalProvider, PremiumDriveProvider>();
        services.AddSingleton<ICarRentalProvider, BudgetWheelsProvider>();
        services.AddSingleton<PricingService>();
        services.AddSingleton<IDocumentValidator, DocumentValidator>();
        services.AddSingleton<SearchService>();
        services.AddSingleton<BookingService>();

        return services;
    }
}
