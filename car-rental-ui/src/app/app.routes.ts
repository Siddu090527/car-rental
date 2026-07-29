import { Routes } from '@angular/router';

import { Search } from './pages/search/search';
import { Booking } from './pages/booking/booking';

export const routes: Routes = [

  {
    path: '',
    component: Search
  },

  {
    path: 'booking',
    component: Booking
  },

  {
    path: '**',
    redirectTo: ''
  }

];