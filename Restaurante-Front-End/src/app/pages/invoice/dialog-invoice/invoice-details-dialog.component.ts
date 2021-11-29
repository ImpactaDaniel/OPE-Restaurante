import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  templateUrl: './invoice-details-dialog.component.html',
  styleUrls: ['./invoice-details-dialog.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class InvoiceDetailsDialogComponent implements OnInit {

  public invoice: any;
  public invoiceTotalPrice: number;

  public columnsToDisplay = ['productId', 'productName', 'quantity', 'accompaniments', 'price', 'obs']
  public columnsToDisplayLogs = ['logId', 'date', 'type', 'message']

  public invoiceStatusDescription = [
    {id: 0, name: 'Criado'}, {id: 1, name: 'Aceito'}, {id: 2, name: 'Rejeitado'}, {id: 3, name: 'Pagamento Pendente'},
    {id: 4, name: 'Pago'}, {id: 5, name: 'Enviado'}, {id: 6, name: 'Entregue'}, {id: 7, name: 'Fechado'}
  ]
  public invoiceLogType = [
    {id: 0, name: 'Criado'}, {id: 1, name: 'Atualizado'}, {id: 2, name: 'Deletado'}, {id: 3, name: 'Fechado'}
  ]

  public expandedElement: any;

  public invoiceLines = new MatTableDataSource<any>();
  public invoiceLogs = new MatTableDataSource<any>();

  constructor(@Inject(MAT_DIALOG_DATA) private data: any, @Inject('BASE_URL') public url: string) { }

  ngOnInit(): void {
    this.invoice = this.data?.invoice;
    this.invoiceLines.data = this.data?.invoice?.products;
    this.invoiceLogs.data = this.data?.invoice?.logs;
    this.invoiceTotalPrice = this.invoiceLines.data.map(x => x.product.price * x.quantity)
                                                    .reduce((a, b) => a+b, 0)
  }
}
