using CarRental.Api.Interfaces;
using CarRental.Api.Pricing;
using CarRental.Api.Providers;
using CarRental.Api.Repositories;
using CarRental.Api.Services;
using CarRental.Api.Validators;

namespace CarRental.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCarRentalServices(this IServiceCollection services)
    {
        // Providers don't keep request-specific state,
        // so a single shared instance is sufficient.
        services.AddSingleton<ICarRentalProvider, PremiumDriveProvider>();
        services.AddSingleton<ICarRentalProvider, BudgetWheelsProvider>();

        // Pricing logic is stateless, so Singleton is appropriate.
        services.AddSingleton<PricingService>();

        // Validation is also stateless.
        services.AddSingleton<IDocumentValidator, DocumentValidator>();

        // SearchService only orchestrates providers and doesn't use the database.
        services.AddSingleton<SearchService>();

        // Repository communicates with SQL Server through AppDbContext,
        // so it must be Scoped.
        services.AddScoped<IBookingRepository, BookingRepository>();

        // BookingService depends on the repository,
        // so it should also be Scoped.
        services.AddScoped<BookingService>();

        return services;
    }
}