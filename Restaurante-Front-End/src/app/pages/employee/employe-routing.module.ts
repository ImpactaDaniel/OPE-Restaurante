import { AuthenticateGuardService } from './authenticate-guard.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeGuard } from './employee.guard';
import { LoginEmployeeComponent } from './login-employee/login-employee';

const routes : Routes = [
  {
    path: 'create',
    component: CreateEmployeeComponent,
    canActivate: [EmployeeGuard]
  },
  {
    path: 'edit',
    component: EditEmployeeComponent,
    canActivate: [EmployeeGuard]
  },
  {
    path: 'login',
    component: LoginEmployeeComponent,
    canActivate: [AuthenticateGuardService]
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
