import { MatInputModule } from '@angular/material/input';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { BankRoutes } from './bank.routing';

@NgModule({
  imports: [
    CommonModule,
    BankRoutes
  ],
})
export class BankModule { }
