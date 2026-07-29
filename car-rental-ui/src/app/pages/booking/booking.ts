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

  bookingReference = '';

  booking: BookingDetails | null = null;

  errorMessage = '';

  searchBooking(): void {

    this.errorMessage = '';
    this.booking = null;

    if (!this.bookingReference.trim()) {
      this.errorMessage = 'Please enter a booking reference.';
      return;
    }

    this.service
      .getBooking(this.bookingReference)
      .subscribe({

        next: response => {

          this.booking = response;

        },

        error: (error: HttpErrorResponse) => {

          console.error(error);

          this.errorMessage =
            error.error?.message ??
            error.error ??
            'Booking not found.';

        }

      });

  }

}