import { ProviderVehicle } from './provider-vehicle';

export interface BookingDetails {
  bookingReferenceNumber: string;
  driverName: string;
  documentType: number;
  documentNumber: string;
  pickupLocation: string;
  provider: number;
  selectedVehicle: ProviderVehicle;
  totalPrice: number;
  cancellationPolicy: string;
  pickupDate: string;
  returnDate: string;
}