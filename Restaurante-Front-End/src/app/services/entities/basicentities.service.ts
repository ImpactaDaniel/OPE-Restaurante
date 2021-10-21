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
    return this.httpClient.get<APIResponse<any>>(`${this.url}${basicEntity.url}/GetAll?page=${basicEntity.page}&size=${basicEntity.pageSize}`);
  }

  public search(basicEntityQuery: any): Observable<APIResponse<any>> {
    return this.httpClient.get<APIResponse<any>>(`${this.url}${basicEntityQuery.url}/Search?field=${basicEntityQuery.field}&value=${basicEntityQuery.value}&page=${basicEntityQuery.page}&size=${basicEntityQuery.pageSize}`);
  }

  public get(basicEntityQuery: any): Observable<APIResponse<any>> {
    return this.httpClient.get<APIResponse<any>>(`${this.url}${basicEntityQuery.url}/Get/id=${basicEntityQuery.id}}`);
  }
}
