import { ProviderVehicle } from './provider-vehicle';

export interface BookingDetails {
  bookingReferenceNumber: string;
  driverName: string;
  provider: string;
  pickupLocation: string;
  pickupDate: string;
  returnDate: string;
  totalPrice: number;
  cancellationPolicy: string;
  documentType: string;
  documentNumber: string;
}