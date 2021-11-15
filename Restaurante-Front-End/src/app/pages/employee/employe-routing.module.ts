import { RouteGuardService } from 'src/app/services/route-guard.service';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { RouteGuardAdminService } from './../../services/route-guard-admin.service';
import { AuthenticateGuardService } from './authenticate-guard.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeGuard } from './employee.guard';
import { LoginEmployeeComponent } from './login-employee/login-employee';
import { ListEmployeeComponent } from './list-employee/list-employee.component';

const routes : Routes = [
  {
    path: 'list',
    component: ListEmployeeComponent,
    canActivate: [RouteGuardAdminService]
  },
  {
    path: 'create',
    component: CreateEmployeeComponent,
    canActivate: [RouteGuardAdminService]
  },
  {
    path: 'edit',
    component: EditEmployeeComponent,
    canActivate: [RouteGuardAdminService]
  },
  {
    path: 'login',
    component: LoginEmployeeComponent,
    canActivate: [AuthenticateGuardService]
  },
  {
    path: 'change-password',
    component: ChangePasswordComponent,
    canActivate: [RouteGuardService]
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
