import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APIResponse } from '../../models/common/apiResponse';
import { Employee } from '../../models/funcionario/funcionario';

@Injectable({
  providedIn: 'root'
})

export class EmployeeService {

  constructor(@Inject('BASE_URL') private url: string, private httpClient: HttpClient) { }

  createEmployee(employee: Employee): Observable<APIResponse<boolean>> {
    return this.httpClient.post<APIResponse<boolean>>(this.url + 'Funcionarios/CreateNew', employee)
  }
}
