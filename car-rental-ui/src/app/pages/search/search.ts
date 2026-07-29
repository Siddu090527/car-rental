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

  bookingResponse: BookingResponse | null = null;

  selectedVehicle?: ProviderVehicle;

  driverName = '';

  documentNumber = '';

  // Booking form state
  showBookingForm = false;

  bookingVehicle?: ProviderVehicle;

  bookingDriverName = '';

  bookingDocumentNumber = '';

  bookingDocumentType = DocumentType.NationalId;

  search(): void {

    this.errorMessage = '';
    this.bookingResponse = null;
    this.vehicles = [];

    this.service.searchCars({
      pickupLocation: this.pickupLocation,
      pickupDate: this.pickupDate,
      returnDate: this.returnDate,
      category: this.category,
      sortByPrice: false
    })
    .subscribe({

      next: response => {

        console.log('Search Response', response);

        this.vehicles = response.vehicles;

      },

      error: (error: HttpErrorResponse) => {

        console.error(error);

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

  confirmBooking(): void {

    if (!this.bookingVehicle) {
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

          this.bookingResponse = response;

          this.selectedVehicle = this.bookingVehicle;

          this.driverName = this.bookingDriverName;

          this.documentNumber = this.bookingDocumentNumber;

          this.showBookingForm = false;

          this.errorMessage = '';

        },

        error: (error: HttpErrorResponse) => {

          console.error(error);

          this.errorMessage =
            error.error?.message ??
            error.error ??
            error.message;

        }

      });

  }

}