import { RouteGuardService } from 'src/app/services/route-guard.service';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { LogoModule } from './../../../components/logo/logo.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditProductComponent } from './edit-product.component';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: EditProductComponent,
    canActivate: [RouteGuardService]
  }
]

@NgModule({
  imports: [
    CommonModule,
    MatInputModule,
    LogoModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    MatSlideToggleModule,
    MatSelectModule
  ],
  declarations: [EditProductComponent]
})
export class EditProductModule { }
