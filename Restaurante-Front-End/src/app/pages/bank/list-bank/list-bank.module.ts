import { MatIconModule } from '@angular/material/icon';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListBankComponent } from './list-bank.component';
import { MatTableModule } from '@angular/material/table';
import { RouterModule, Routes } from '@angular/router';
import { MatCardModule } from '@angular/material/card';

const routes: Routes = [
  {
    path: '',
    component: ListBankComponent
  }
]


@NgModule({
  declarations: [ListBankComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    MatCardModule,
    RouterModule.forChild(routes)
  ]
})
export class ListBankModule { }
