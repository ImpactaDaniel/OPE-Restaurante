import { Employee } from './../../../models/funcionario/employee';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APIResponse } from '../../../models/common/apiResponse';

@Injectable({
  providedIn: 'root'
})

export class EmployeeService {

  private urlInvoices: string = 'Employees'

  constructor(@Inject('BASE_URL') private url: string, private httpClient: HttpClient) { }

  createEmployee(employee: Employee): Observable<APIResponse<boolean>> {
    return this.httpClient.post<APIResponse<boolean>>(this.url + 'Employees/Create', employee)
  }

  public getAllInvoices(): any {
    return this.httpClient.get<any>(
      this.url + this.urlInvoices + "/GetAll"
      );
  }
}
