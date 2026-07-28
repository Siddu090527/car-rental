import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CarRentalService } from '../../core/services/car-rental.service';
import { ProviderVehicle } from '../../models/provider-vehicle';

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

  search(): void {

    this.service.searchCars({
      pickupLocation: this.pickupLocation,
      pickupDate: this.pickupDate,
      returnDate: this.returnDate,
      category: this.category,
      sortByPrice: false
    })
    .subscribe({
      next: response => {
        this.vehicles = response.vehicles;
      },
      error: error => {
        console.error(error);
        alert('Unable to search vehicles.');
      }
    });
  }
}