import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'create',
    loadChildren: () => import('./create-product/create-product.module').then(m => m.CreateProductModule)
  },
  {
    path: 'list',
    loadChildren: () => import('./list-product/list-product.module').then(m => m.ListProductModule)
  }
];

export const ProductsRoutes = RouterModule.forChild(routes);
