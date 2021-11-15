import { RouteGuardAdminService } from './../../services/route-guard-admin.service';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'create',
    loadChildren: () => import('./create/create.bank.module').then(m => m.CreateBankModule)
  },
  {
    path: 'list',
    loadChildren: () => import('./list-bank/list-bank.module').then(m => m.ListBankModule)
  }
];

export const BankRoutes = RouterModule.forChild(routes);
