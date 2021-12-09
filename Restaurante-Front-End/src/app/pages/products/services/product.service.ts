import { APIResponse } from './../../../models/common/apiResponse';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private client: HttpClient, @Inject('BASE_URL') private url: string) { }

  public createProduct(product: any) {
    return this.client.post<APIResponse<any>>(this.url + "Products/Create", product);
  }

  public updateProduct(product: any, id: number) {
    return this.client.put<APIResponse<any>>(this.url + 'Products/Update/' + id, product);
  }

  public getAllProducts(page: number, limit: number) {
    return this.client.get<any>(this.url + "Products/GetAll?page=" + page + "&limit=" + limit);
  }

  public searchProducts(field: string, value: string, page: number, limit: number) {
    return this.client.get<any>(this.url + "Products/Search?page=" + page + "&limit=" + limit + "&field=" + field + "&value=" + value);
  }

  public getProduct(id: number) {
    return this.client.get<APIResponse<any>>(this.url + "Products/Get/" + id);
  }

  public deleteProduct(id: number) {
    return this.client.delete<APIResponse<any>>(this.url + "Products/Delete/" + id);
  }

}
