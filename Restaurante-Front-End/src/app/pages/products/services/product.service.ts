import { APIResponse } from './../../../models/common/apiResponse';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private client: HttpClient, @Inject('BASE_URL') private url: string) { }

  public createProduct(product: any) {
    let header = new HttpHeaders();

    header.append('Content-Type', 'application/json');
    return this.client.post<APIResponse<any>>(this.url + "Products/Create", product, {
      headers: header
    });
  }

}
