using CarRental.Api.Enums;
using CarRental.Api.Models;

namespace CarRental.Api.Pricing;

public class PricingService
{
    public PriceBreakdown CalculatePrice(ProviderType providerType, decimal dailyRate, DateTime pickupDate, DateTime returnDate)
    {
        var rentalNights = Math.Max(1, (returnDate.Date - pickupDate.Date).Days);
        var dailySubtotal = dailyRate * rentalNights;
        var surcharge = 0m;

        if (providerType == ProviderType.BudgetWheels)
        {
            for (var day = pickupDate.Date; day < returnDate.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday or DayOfWeek.Sunday)
                {
                    surcharge += dailyRate * 0.2m;
                }
            }
        }

        return new PriceBreakdown
        {
            DailyRate = dailyRate,
            RentalNights = rentalNights,
            Surcharge = surcharge,
            TotalPrice = providerType == ProviderType.PremiumDrive ? dailySubtotal : dailySubtotal + surcharge
        };
    }
}
