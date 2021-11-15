import { RouteGuardService } from './../../../services/route-guard.service';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { LogoModule } from './../../../components/logo/logo.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateProductComponent } from './create-product.component';
import { RouterModule, Routes } from '@angular/router';
import { IConfig, NgxMaskModule } from 'ngx-mask';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: CreateProductComponent,
    canActivate: [RouteGuardService]
  }
]

const maskConfig: Partial<IConfig> = {
  dropSpecialCharacters: false
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LogoModule,
    MatInputModule,
    MatSlideToggleModule,
    NgxMaskModule.forRoot(maskConfig),
    ReactiveFormsModule,
    MatSelectModule
  ],
  declarations: [CreateProductComponent]
})
export class CreateProductModule { }
