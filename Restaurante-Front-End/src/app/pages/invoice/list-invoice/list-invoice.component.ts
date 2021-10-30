import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { BasicentitiesService } from 'src/app/services/entities/basicentities.service';
import { InvoiceService } from '../services/invoice.service';

@Component({
  selector: 'app-list-invoice.component',
  templateUrl: './list-invoice.component.html',
  styleUrls: ['./list-invoice.component.scss']
})
export class ListInvoiceComponent implements OnInit {

  public invoices = new MatTableDataSource<any>();

  public limit: number = 5;
  public page: number = 0;
  public listSize: number = 0;
  private isSearching = false;
  public name: string;
  public invoiceStatusDescription = ['Criado','Aceito', 'Rejeitado', 'Pagamento Pendente', 'Pago', 'Enviado', 'Entregue', 'Fechado']
  public paymentTypeDescription = ['Débito', 'Crédito']

  public displayedColumns = ['id', 'name', 'street', 'number', 'cep', 'status', 'payment', 'remove', 'details']

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
    let value = this.name;

    this.isSearching = true;

    if(value.length <= 2) {
      this.loadTable();
      this.isSearching = false;
      return;
    }

    this.invoiceService.search({page: this.page, limit: this.limit, field: 'product', value: value}).subscribe(
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
