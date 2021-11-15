import { Routes, RouterModule } from '@angular/router';
import { RouteGuardService } from 'src/app/services/route-guard.service';

const routes: Routes = [
  {
    path: 'list',
    loadChildren: () => import('./list-invoice/list-invoice.module').then(m => m.ListInvoiceModule)
  }
];

export const InvoiceRoutes = RouterModule.forChild(routes);
