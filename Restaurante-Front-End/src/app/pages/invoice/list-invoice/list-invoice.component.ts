import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { InvoiceService } from '../services/invoice.service';

@Component({
  selector: 'app-list-invoice.component',
  templateUrl: './list-invoice.component.html',
  styleUrls: ['./list-invoice.component.scss']
})
export class ListInvoiceComponent implements OnInit {

  public invoices = new MatTableDataSource<any>();

  public statusChoice: string;
  public limit: number = 5;
  public page: number = 0;
  public listSize: number = 0;
  private isSearching = false;
  public status: string;
  public invoiceStatusDescription = [
    {id: 0, name: 'Criado'}, {id: 1, name: 'Aceito'}, {id: 2, name: 'Rejeitado'}, {id: 3, name: 'Pagamento Pendente'}, 
    {id: 4, name: 'Pago'}, {id: 5, name: 'Enviado'}, {id: 6, name: 'Entregue'}, {id: 7, name: 'Fechado'}
  ]
  public paymentTypeDescription = ['Débito', 'Crédito']

  public displayedColumns = ['invoiceId', 'name', 'customerId', 'street', 'number', 'cep', 'payment', 'status', 'changeStatus', 'remove', 'details']

  constructor(
    private invoiceService: InvoiceService,
    private alertService: AlertService,
    private router: Router
    ) { }

  ngOnInit() {
    this.loadTable();
  }

  private loadTable() {
    this.invoiceService.getAll({page: this.page, limit: this.limit}).subscribe(
      r => {
        console.log(r)
        this.invoices.data = r.response.result.entities
        this.listSize = r.response.result.size;
      },
      err => {
        console.log(err)
      }
    )
  }

  public search(){
    let value = this.status;

    this.isSearching = true;

    if(value.length <= 2) {
      this.loadTable();
      this.isSearching = false;
      return;
    }

    this.invoiceService.search({page: this.page, limit: this.limit, status: value}).subscribe(
      r => {
        console.log(r)
        this.invoices.data = r.response.result.entities
        this.listSize = r.response.result.size;
      },
      err => {
        console.log(err)
      }
    )
  }

  public getStatusChoice(status: any) {
    this.statusChoice = status
  }

  public invoiceStatusChange(invoiceId: any, statusCode: any) {
    this.invoiceService.invoiceStatusChange({ id: invoiceId, status: this.statusChoice }).subscribe(
      r => {
        console.log(r)
        this.loadTable()
      }
    )
  }

  public details(id: number) {
    console.log(id);
    console.log(this.invoices.data)
    // let productsList = this.invoices.data.filter(item => item.id === id)
  }

  public remove(id: number) {
    console.log(id);
  }

  public changePaginator(event: any) {
    this.limit = event.pageSize;
    this.page = event.pageIndex;
    if(!this.isSearching){
      this.loadTable();
      return;
    }

    this.search()
  }

}
