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

  public createEmployee(employee: Employee): Observable<APIResponse<boolean>> {
    return this.httpClient.post<APIResponse<boolean>>(this.url + 'Employees/Create', employee)
  }

  public getEmployeeById(invoiceId: number): any {
    return this.httpClient.get<any>(
      this.url + this.urlInvoices + "/Get/" + invoiceId
    );
  }

  public getAllEmployees(page: number, limit: number): any {
    return this.httpClient.get<any>(
      this.url + this.urlInvoices + "/GetAll?page=" + page + "&limit=" + limit
      );
  }

  public searchEmployees(field: string, value: string, page: number, limit: number): any {
    return this.httpClient.get<APIResponse<any>>(
      this.url + this.urlInvoices + "/Search?&field=" + field + "&value=" + value + "&page=" + page + "&limit=" + limit
      );
  }
}
