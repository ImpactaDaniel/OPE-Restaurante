import { LogoModule } from './../../components/logo/logo.module';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateEmployeeComponent } from './create-employee/create-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeRoutingModule } from './employe-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginEmployeeComponent } from './login-employee/login-employee';
import { NgxMaskModule } from 'ngx-mask';




@NgModule({
  declarations: [CreateEmployeeComponent, EditEmployeeComponent, LoginEmployeeComponent],
  imports: [
    CommonModule,
    EmployeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgxMaskModule.forRoot(),
    MatInputModule,
    LogoModule
  ]
})
export class EmployeeModule { }
