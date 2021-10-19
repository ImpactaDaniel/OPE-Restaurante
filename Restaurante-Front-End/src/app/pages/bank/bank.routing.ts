import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';

const routes: Routes = [
  {
    path: 'create',
    component: CreateComponent
  },
];

export const BankRoutes = RouterModule.forChild(routes);
