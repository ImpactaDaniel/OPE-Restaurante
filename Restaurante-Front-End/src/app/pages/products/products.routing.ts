import { Routes, RouterModule } from '@angular/router';
import { RouteGuardService } from 'src/app/services/route-guard.service';

const routes: Routes = [
  {
    path: 'create',
    loadChildren: () => import('./create-product/create-product.module').then(m => m.CreateProductModule)
  },
  {
    path: 'list',
    loadChildren: () => import('./list-product/list-product.module').then(m => m.ListProductModule)
  },
  {
    path: 'edit',
    loadChildren: () => import('./edit-product/edit-product.module').then(m => m.EditProductModule)
  }
];

export const ProductsRoutes = RouterModule.forChild(routes);
