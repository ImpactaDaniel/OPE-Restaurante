import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';

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
