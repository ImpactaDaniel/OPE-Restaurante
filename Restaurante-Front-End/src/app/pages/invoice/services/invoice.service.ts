import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { APIResponse } from 'src/app/models/common/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private hubConnection: HubConnection;
  private urlInvoices: string = 'Invoice'

  constructor(@Inject("BASE_URL") private url: string, private httpClient: HttpClient) { }

  public init() {
    this.buildConnection();
    this.startConnection();
    this.registerOnServerEvents();
  }

  public getAll(pagination: any): any {
    return this.httpClient.get<any>(
      `${this.url}${this.urlInvoices}/GetAll?page=${pagination.page}&limit=${pagination.limit}`
      );
  }

  public search(pagination: any): any {
    return this.httpClient.get<APIResponse<any>>(
      `${this.url}${this.urlInvoices}/
      Search?field=${pagination.field}&
      value=${pagination.value}&
      page=${pagination.page}&
      size=${pagination.limit}`
      );
  }

  public invoiceStatusChange(statusChange: any) {
    return this.httpClient.patch<any>(
      `${this.url}${this.urlInvoices}/UpdateStatus/${statusChange.id}?status=${statusChange.status}`, statusChange
    )
  }

  private buildConnection() {
    this.hubConnection = new HubConnectionBuilder()
                              .withUrl(`${this.url}invoice-notification-hub`)
                              .build();
  }

  public stopConnection() {
    this.hubConnection.stop();
  }

  private startConnection() {
    this.hubConnection
        .start()
        .then(() => {
          console.log('Hub connected!');
        })
        .catch(() => {
          setTimeout(() => {
            this.startConnection();
          }, 5000);
        });
  }

  private registerOnServerEvents() {
    this.hubConnection.on('NewInvoiceNotification', (invoice, type) => {
      console.log(invoice, type);
    });
  }

 
}
