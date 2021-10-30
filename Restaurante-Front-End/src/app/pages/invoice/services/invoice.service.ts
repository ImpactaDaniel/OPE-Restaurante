import { Inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { EventEmitter } from 'events';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private hubConnection: HubConnection;

  public emmiter = new EventEmitter();

  constructor(@Inject("BASE_URL") private url: string) { }

  public init() {
    this.buildConnection();
    this.startConnection();
    this.registerOnServerEvents();
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
