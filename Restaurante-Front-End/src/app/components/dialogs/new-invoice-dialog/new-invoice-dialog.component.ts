import { MatTableDataSource } from '@angular/material/table';
import { Component, ElementRef, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { InvoiceService } from 'src/app/pages/invoice/services/invoice.service';
import { InvoiceStatus } from 'src/app/models/common/invoiceStatus';

@Component({
  templateUrl: './new-invoice-dialog.component.html',
  styleUrls: ['./new-invoice-dialog.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class NewInvoiceDialogComponent implements OnInit {

  public invoice: any;

  public columnsToDisplay = ['productName', 'quantity', 'obs']

  public expandedElement: any;

  public invoiceLines = new MatTableDataSource<any>();

  constructor(@Inject(MAT_DIALOG_DATA) private data: any, @Inject('BASE_URL') public url: string, private invoiceService: InvoiceService) { }

  ngOnInit(): void {
    this.invoice = this.data?.invoice;
    this.invoiceLines.data = this.data?.invoice?.Products.$values;
  }

  public updateInvoiceStatus(accepted: boolean) {
    console.log('here');
    this.invoiceService.updateStatus(this.invoice.id, accepted ? InvoiceStatus.Accepted : InvoiceStatus.Rejected)
  }

}
