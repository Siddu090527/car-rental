import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

import { CarRentalService } from '../../core/services/car-rental.service';
import { ProviderVehicle } from '../../models/provider-vehicle';
import { BookingRequest } from '../../models/booking-request';
import { BookingResponse } from '../../models/booking-response';
import { DocumentType } from '../../models/document-type';
import { ProviderType } from '../../models/provider-type';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './search.html',
  styleUrl: './search.scss'
})
export class Search {

  private readonly service = inject(CarRentalService);

  pickupLocation = '';
  pickupDate = '';
  returnDate = '';
  category?: number;

  vehicles: ProviderVehicle[] = [];

  errorMessage = '';

  loading = false;

  hasSearched = false;

  bookingResponse: BookingResponse | null = null;

  showBookingForm = false;

  bookingVehicle?: ProviderVehicle;

  bookingDriverName = '';

  bookingDocumentNumber = '';

  bookingDocumentType = DocumentType.NationalId;

  selectedVehicle?: ProviderVehicle;

  driverName = '';

  documentNumber = '';

  search(): void {

    this.errorMessage = '';
    this.loading = true;
    this.hasSearched = true;
    this.bookingResponse = null;
    this.showBookingForm = false;
    this.vehicles = [];

    if (!this.pickupLocation.trim()) {
      this.loading = false;
      this.errorMessage = 'Pickup location is required.';
      return;
    }

    if (!this.pickupDate) {
      this.loading = false;
      this.errorMessage = 'Pickup date is required.';
      return;
    }

    if (!this.returnDate) {
      this.loading = false;
      this.errorMessage = 'Return date is required.';
      return;
    }

    if (this.returnDate <= this.pickupDate) {
      this.loading = false;
      this.errorMessage = 'Return date must be after pickup date.';
      return;
    }

    this.service.searchCars({
      pickupLocation: this.pickupLocation,
      pickupDate: this.pickupDate,
      returnDate: this.returnDate,
      category: this.category,
      sortByPrice: false
    })
    .subscribe({

      next: response => {

        console.log('Search Response:', response);

        console.log('Vehicle Count:', response.vehicles.length);

        console.log('Vehicles:', response.vehicles);

        this.vehicles = [...response.vehicles];

        console.log('After assignment:', this.vehicles);

        this.loading = false;

        this.loading = false;

      },

      error: (error: HttpErrorResponse) => {

        console.error(error);

        this.loading = false;

        this.errorMessage =
          error.error?.message ??
          error.error ??
          error.message;

      }

    });

  }

    book(vehicle: ProviderVehicle): void {

    this.errorMessage = '';

    this.bookingVehicle = vehicle;

    this.bookingDriverName = '';

    this.bookingDocumentNumber = '';

    this.bookingDocumentType = DocumentType.NationalId;

    this.showBookingForm = true;

  }

  cancelBooking(): void {

    this.showBookingForm = false;

    this.bookingVehicle = undefined;

    this.bookingDriverName = '';

    this.bookingDocumentNumber = '';

    this.bookingDocumentType = DocumentType.NationalId;

    this.errorMessage = '';

  }

  confirmBooking(): void {

    if (!this.bookingVehicle) {

      this.errorMessage = 'No vehicle selected.';

      return;

    }

    if (!this.bookingDriverName.trim()) {

      this.errorMessage = 'Driver name is required.';

      return;

    }

    if (!this.bookingDocumentNumber.trim()) {

      this.errorMessage = 'Document number is required.';

      return;

    }

    this.loading = true;

    this.errorMessage = '';

    const request: BookingRequest = {

      driverName: this.bookingDriverName,

      documentType: this.bookingDocumentType,

      documentNumber: this.bookingDocumentNumber,

      pickupLocation: this.pickupLocation,

      provider:
        this.bookingVehicle.provider === 'PremiumDrive'
          ? ProviderType.PremiumDrive
          : ProviderType.BudgetWheels,

      selectedVehicle: this.bookingVehicle,

      pickupDate: this.pickupDate,

      returnDate: this.returnDate

    };

    this.service
      .bookCar(request)
      .subscribe({

        next: (response: BookingResponse) => {

          this.loading = false;

          this.bookingResponse = response;

          this.selectedVehicle = this.bookingVehicle;

          this.driverName = this.bookingDriverName;

          this.documentNumber = this.bookingDocumentNumber;

          this.showBookingForm = false;

          console.log('Booking Response:', response);

        },

        error: (error: HttpErrorResponse) => {

          this.loading = false;

          console.error(error);

          this.errorMessage =
            error.error?.message ??
            error.error ??
            'Booking failed.';

        }

      });

  }

}