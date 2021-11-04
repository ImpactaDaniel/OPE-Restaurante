import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InvoiceRoutes } from './invoice.routing';
import { InvoiceDetailsDialogModule } from './dialog-invoice/invoice-details-dialog.module';

@NgModule({
  imports: [
    CommonModule,
    InvoiceDetailsDialogModule,
    InvoiceRoutes
  ]
})
export class InvoiceModule { }
