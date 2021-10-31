import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { APIResponse } from 'src/app/models/common/apiResponse';
import { EventEmitter } from 'events';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private hubConnection: HubConnection;
  private urlInvoices: string = 'Invoice'

  public emmiter = new EventEmitter();
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
      `${this.url}${this.urlInvoices}/Search?&page=${pagination.page}&size=${pagination.limit}&status=${pagination.status}`
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
        .catch(error => {
          console.log(error);
          setTimeout(() => {
            this.startConnection();
          }, 5000);
        });
  }

  private registerOnServerEvents() {
    this.hubConnection.onclose( error => {
      console.error(error);
      setTimeout(() => {
        this.startConnection();
      }, 5000);
    })
    this.hubConnection.on('newInvoiceNotification', (invoice, type) => {
      this.emmiter.emit('newInvoice', invoice, type);
    });
  }

 
}
