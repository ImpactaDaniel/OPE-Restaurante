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

  public getAllInvoices(page: number, limit: number): any {
    return this.httpClient.get<any>(
      this.url + this.urlInvoices + "/GetAll?page=" + page + "&limit=" + limit
      );
  }

  public searchInvoices(field: string, value: string, page: number, limit: number): any {
    return this.httpClient.get<APIResponse<any>>(
      this.url + this.urlInvoices + "/Search?&field=" + field + "&value=" + value + "&page=" + page + "&limit=" + limit
      );
  }

  public invoiceStatusChange(changeStatus: any) {
    return this.httpClient.patch<any>(
      this.url + this.urlInvoices + "/UpdateStatus/"+ changeStatus.id + "?status=" + changeStatus.status, changeStatus
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
          console.error(error);
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
