import { CreateDeliverymanComponent } from './create-deliveryman/create-deliveryman.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'create',
    component: CreateDeliverymanComponent
  },
  {
    path: '**',
    redirectTo: 'create'
  }
];

export const DeliverymanRoutes = RouterModule.forChild(routes);
