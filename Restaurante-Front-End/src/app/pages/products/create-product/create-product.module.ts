import { MatInputModule } from '@angular/material/input';
import { LogoModule } from './../../../components/logo/logo.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateProductComponent } from './create-product.component';
import { RouterModule, Routes } from '@angular/router';
import { NgxMaskModule } from 'ngx-mask';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

const routes: Routes = [
  {
    path: '',
    component: CreateProductComponent
  }
]

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LogoModule,
    MatInputModule,
    MatSlideToggleModule,
    NgxMaskModule.forRoot(),
  ],
  declarations: [CreateProductComponent]
})
export class CreateProductModule { }
