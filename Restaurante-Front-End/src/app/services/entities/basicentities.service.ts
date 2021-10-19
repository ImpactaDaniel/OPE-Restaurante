import { APIResponse } from './../../models/common/apiResponse';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasicentitiesService {

  constructor(private httpClient: HttpClient, @Inject("BASE_URL") private url: string) { }

  public createEntity(basicEntity: any): Observable<APIResponse<boolean>>{
    return this.httpClient.post<APIResponse<boolean>>(`${this.url}${basicEntity.url}/Create`, basicEntity);
  }

  public getAll(basicEntity: any): Observable<APIResponse<any>> {
    return this.httpClient.get<APIResponse<any>>(`${this.url}${basicEntity.url}/GetAll`);
  }
}
