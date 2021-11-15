import { RouteGuardService } from 'src/app/services/route-guard.service';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { RouterModule, Routes } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatButtonModule} from '@angular/material/button';
import { ListInvoiceComponent } from './list-invoice.component';
import { MatSelectModule } from '@angular/material/select';


const routes: Routes = [
  {
    path: '',
    component: ListInvoiceComponent,
    canActivate: [RouteGuardService]
  }
]

@NgModule({
  declarations: [ListInvoiceComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    FormsModule,
    MatInputModule,
    MatPaginatorModule,
    MatCardModule,
    MatButtonModule,
    MatSelectModule,
    RouterModule.forChild(routes)
  ]
})
export class ListInvoiceModule { }
