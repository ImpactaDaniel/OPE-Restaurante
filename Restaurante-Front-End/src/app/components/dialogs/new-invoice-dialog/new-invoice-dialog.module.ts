import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { NewInvoiceDialogComponent } from './new-invoice-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';



@NgModule({
  declarations: [NewInvoiceDialogComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule
  ],
  exports: [NewInvoiceDialogComponent]
})
export class NewInvoiceDialogModule { }
