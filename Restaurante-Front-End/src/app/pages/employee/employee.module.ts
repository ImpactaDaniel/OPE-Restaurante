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
import { ListEmployeeComponent } from './list-employee/list-employee.component';
import { MatCardModule } from '@angular/material/card'
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { EmployeeDialogModule } from './dialog-employee/employee-dialog.module';


@NgModule({
  declarations: [CreateEmployeeComponent, EditEmployeeComponent, LoginEmployeeComponent, ListEmployeeComponent],
  imports: [
    CommonModule,
    MatTableModule,
    MatIconModule,
    EmployeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    NgxMaskModule.forRoot(),
    MatInputModule,
    LogoModule,
    MatCardModule,
    MatPaginatorModule,
    EmployeeDialogModule
  ]
})
export class EmployeeModule { }
