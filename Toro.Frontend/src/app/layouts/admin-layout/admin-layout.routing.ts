import { Routes } from '@angular/router';

import { HomeComponent } from '../../home/home.component';
import { OrderComponent } from 'app/order/order.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: HomeComponent },
    { path: 'order',        component: OrderComponent },
];
