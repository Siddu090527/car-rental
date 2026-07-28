using CarRental.Api.Enums;
using CarRental.Api.Models;

namespace CarRental.Api.Pricing;

/// <summary>
/// Calculates rental pricing for supported providers.
/// </summary>
public sealed class PricingService
{
    /// <summary>
    /// Calculates the total rental price.
    /// </summary>
    public PriceBreakdown CalculatePrice(
        ProviderType providerType,
        decimal dailyRate,
        DateTime pickupDate,
        DateTime returnDate)
    {
        if (dailyRate <= 0)
        {
            throw new InvalidOperationException(
                "Daily rate must be greater than zero.");
        }

        if (returnDate <= pickupDate)
        {
            throw new InvalidOperationException(
                "Return date must be after pickup date.");
        }

        var rentalNights = (returnDate.Date - pickupDate.Date).Days;
        var dailySubtotal = dailyRate * rentalNights;
        decimal surcharge = 0m;

        if (providerType == ProviderType.BudgetWheels)
        {
            for (var day = pickupDate.Date; day < returnDate.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Friday ||
                    day.DayOfWeek == DayOfWeek.Saturday ||
                    day.DayOfWeek == DayOfWeek.Sunday)
                {
                    surcharge += dailyRate * 0.20m;
                }
            }
        }

        return new PriceBreakdown
        {
            DailyRate = dailyRate,
            RentalNights = rentalNights,
            Surcharge = surcharge,
            TotalPrice = providerType == ProviderType.PremiumDrive
                ? dailySubtotal
                : dailySubtotal + surcharge
        };
    }
}