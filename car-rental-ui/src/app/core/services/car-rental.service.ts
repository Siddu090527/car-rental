import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { CarSearchRequest } from '../../models/car-search-request';
import { CarSearchResponse } from '../../models/car-search-response';
import { BookingRequest } from '../../models/booking-request';
import { BookingResponse } from '../../models/booking-response';
import { BookingDetails } from '../../models/booking-details';

@Injectable({
  providedIn: 'root'
})
export class CarRentalService {

  private readonly http = inject(HttpClient);

  // ASP.NET Core API
  private readonly apiUrl = environment.apiUrl;

  searchCars(request: CarSearchRequest): Observable<CarSearchResponse> {

    let params = new HttpParams()
      .set('pickup', request.pickupLocation)
      .set('from', request.pickupDate)
      .set('to', request.returnDate);

    if (request.category !== undefined && request.category !== null) {
      params = params.set(
        'category',
        request.category.toString()
      );
    }

    return this.http.get<CarSearchResponse>(
      `${this.apiUrl}/cars/search`,
      { params }
    );

  }

  bookCar(
    request: BookingRequest
  ): Observable<BookingResponse> {

    return this.http.post<BookingResponse>(
      `${this.apiUrl}/cars/book`,
      request
    );

  }

  getBooking(
    reference: string
  ): Observable<BookingDetails> {

    return this.http.get<BookingDetails>(
      `${this.apiUrl}/cars/booking/${reference}`
    );

  }

}