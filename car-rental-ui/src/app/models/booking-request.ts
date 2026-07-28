import { ProviderVehicle } from './provider-vehicle';

export interface BookingRequest {
  driverName: string;
  documentType: number;
  documentNumber: string;
  pickupLocation: string;
  provider: number;
  selectedVehicle: ProviderVehicle;
  pickupDate: string;
  returnDate: string;
}