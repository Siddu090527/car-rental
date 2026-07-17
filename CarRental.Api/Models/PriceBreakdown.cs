namespace CarRental.Api.Models;

public class PriceBreakdown
{
    public decimal DailyRate { get; set; }
    public int RentalNights { get; set; }
    public decimal Surcharge { get; set; }
    public decimal TotalPrice { get; set; }
}
