import { ProductsRoutes } from './products.routing';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [
    CommonModule,
    ProductsRoutes
  ]
})
export class ProductsModule { }
