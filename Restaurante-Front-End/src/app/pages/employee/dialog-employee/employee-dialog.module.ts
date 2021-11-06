import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { DialogEmployeeComponent } from './employee-dialog.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatExpansionModule} from '@angular/material/expansion';



@NgModule({
  declarations: [DialogEmployeeComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatTableModule,
    MatExpansionModule
  ],
  exports: [DialogEmployeeComponent]
})
export class EmployeeDialogModule { }
