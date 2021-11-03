import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { InvoiceDetailsDialogComponent } from 'src/app/components/dialogs/invoice-details-dialog/invoice-details-dialog.component';
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
  public searchField = "customerName";
  public searchValue: string;
  public invoice: Object;
  public invoiceStatusDescription = [
    {id: 0, name: 'Criado'}, {id: 1, name: 'Aceito'}, {id: 2, name: 'Rejeitado'}, {id: 3, name: 'Pagamento Pendente'},
    {id: 4, name: 'Pago'}, {id: 5, name: 'Enviado'}, {id: 6, name: 'Entregue'}, {id: 7, name: 'Fechado'}
  ]
  public paymentTypeDescription = ['Débito', 'Crédito']

  public displayedColumns = ['invoiceId', 'name', 'customerId', 'street', 'number', 'cep', 'payment', 'status', 'changeStatus', 'remove', 'details']

  constructor(
    private invoiceService: InvoiceService,
    private dialog: MatDialog,
    private alertService: AlertService,
    private router: Router
    ) { }

  ngOnInit() {
    this.getInvoicesList();
  }

  private getInvoicesList() {

    if (this.isSearching) {
      this.invoiceService.searchInvoices(this.searchField, this.searchValue, this.page, this.limit).subscribe(res => {
        this.invoices.data = res.response.result?.entities;
        this.listSize = res.response.result?.size;
      });
      return;
    }

    this.invoiceService.getAllInvoices(this.page, this.limit).subscribe(res => {
      this.invoices.data = res.response.result.entities;
      this.listSize = res.response.result.size;
    })
  }

  public changePaginator(event: any) {
    this.limit = event.pageSize;
    this.page = event.pageIndex;
    this.getInvoicesList();
  }

  public search(event: any){
    this.searchValue = !isNaN(event.value) ? event.value : event.target.value;

    this.isSearching = true;
    this.page = 0;
    this.limit = 5;
    this.getInvoicesList();
  }

  public getStatusChoice(status: any) {
    this.statusChoice = status
  }

  public invoiceStatusChange(invoiceId: string) {
    this.invoiceService.invoiceStatusChange({ id: invoiceId, status: this.statusChoice }).subscribe(
      r => {
        this.getInvoicesList()
      }
    )
  }

  public async details(id: number) {
    let response = await this.invoiceService.getInvoiceById(id).toPromise()
    let invoice = response.response.result
    this.dialog.open(InvoiceDetailsDialogComponent, {
      maxWidth: '100%',
      data: {
        invoice
      }
    });
  }

  public remove(id: number) {
  }

  public searchFieldChanged() {
    this.searchValue = "";
  }

  public removeFilters() {
    this.isSearching = false;
    this.searchField = "customerName";
    this.searchValue = "";
    this.getInvoicesList();
  }

}
