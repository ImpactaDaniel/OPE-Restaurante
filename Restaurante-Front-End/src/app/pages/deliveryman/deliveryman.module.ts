import { MatInputModule } from '@angular/material/input';
import { CreateDeliverymanComponent } from './create-deliveryman/create-deliveryman.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { DeliverymanRoutes } from './deliveryman.routing';
import { NgxMaskModule } from 'ngx-mask';

@NgModule({
  imports: [
    CommonModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgxMaskModule.forRoot(),
    DeliverymanRoutes
  ],
  declarations: [CreateDeliverymanComponent]
})
export class DeliverymanModule { }
