export interface CarSearchRequest {
  pickupLocation: string;
  pickupDate: string;
  returnDate: string;
  category?: number;
  sortByPrice: boolean;
}