import { Inject, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {

  private hubConnection: HubConnection;

  constructor(@Inject("BASE_URL") private url: string) { }

  public init() {
    // this.buildConnection();
    // this.startConnection();
    // this.registerOnServerEvents();
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
