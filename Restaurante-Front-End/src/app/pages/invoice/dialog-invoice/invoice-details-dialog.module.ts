import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { InvoiceDetailsDialogComponent } from './invoice-details-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatExpansionModule} from '@angular/material/expansion';



@NgModule({
  declarations: [InvoiceDetailsDialogComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatExpansionModule
  ],
  exports: [InvoiceDetailsDialogComponent]
})
export class InvoiceDetailsDialogModule { }
