import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'list',
    loadChildren: () => import('./list-invoice/list-invoice.module').then(m => m.ListInvoiceModule)
  }
];

export const InvoiceRoutes = RouterModule.forChild(routes);
