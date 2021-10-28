import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'create',
    loadChildren: () => import('./create-product/create-product.module').then(m => m.CreateProductModule)
  },
];

export const ProductsRoutes = RouterModule.forChild(routes);
