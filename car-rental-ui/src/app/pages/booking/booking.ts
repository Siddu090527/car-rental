import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

import { CarRentalService } from '../../core/services/car-rental.service';
import { BookingDetails } from '../../models/booking-details';

@Component({
  selector: 'app-booking',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './booking.html',
  styleUrl: './booking.scss'
})
export class Booking {

  private readonly service = inject(CarRentalService);

  reference = '';

  booking?: BookingDetails;

  errorMessage = '';

  loading = false;

  searchBooking(): void {

    this.errorMessage = '';
    this.booking = undefined;

    if (!this.reference.trim()) {

      this.errorMessage = 'Booking reference is required.';

      return;

    }

    this.loading = true;

    this.service
      .getBooking(this.reference)
      .subscribe({

        next: response => {

          this.loading = false;

          this.booking = response;

        },

        error: (error: HttpErrorResponse) => {

          this.loading = false;

          console.error(error);

          this.errorMessage =
            error.error?.message ??
            error.error ??
            'Booking not found.';

        }

      });

  }

}