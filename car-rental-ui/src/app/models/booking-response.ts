export interface BookingResponse {
  bookingReferenceNumber: string;
  provider: string;
  totalPrice: number;
  cancellationPolicy: string;
}