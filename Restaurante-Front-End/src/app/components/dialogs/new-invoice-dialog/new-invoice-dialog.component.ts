import { MatTableDataSource } from '@angular/material/table';
import { Component, ElementRef, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { animate, state, style, transition, trigger } from '@angular/animations';

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

  constructor(@Inject(MAT_DIALOG_DATA) private data: any, @Inject('BASE_URL') public url: string) { }

  ngOnInit(): void {
    this.invoice = this.data?.invoice;
    this.invoiceLines.data = this.data?.invoice?.Products.$values;
  }

}
