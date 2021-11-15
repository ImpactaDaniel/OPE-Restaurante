import { RouteGuardAdminService } from './../../services/route-guard-admin.service';
import { CreateDeliverymanComponent } from './create-deliveryman/create-deliveryman.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'create',
    component: CreateDeliverymanComponent,
    canActivate: [RouteGuardAdminService]
  }
];

export const DeliverymanRoutes = RouterModule.forChild(routes);
