import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeGuard } from './employee.guard';

const routes : Routes = [
  {
    path: 'create',
    component: CreateEmployeeComponent,
    canActivate: [EmployeeGuard]
  },
  {
    path: 'edit',
    component: EditEmployeeComponent
  }
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class EmployeRoutingModule { }
