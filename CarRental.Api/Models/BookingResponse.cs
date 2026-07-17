namespace CarRental.Api.Models;

public class BookingResponse
{
    public string BookingReferenceNumber { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public string CancellationPolicy { get; set; } = string.Empty;
}
