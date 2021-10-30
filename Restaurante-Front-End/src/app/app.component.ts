import { InvoiceService } from './pages/invoice/services/invoice.service';
import { ThrowStmt } from '@angular/compiler';
import { AfterContentChecked, Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { NewInvoiceDialogComponent } from './components/dialogs/new-invoice-dialog/new-invoice-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, AfterContentChecked {
  title = 'app';
  showMenu: Boolean;
  /**
   *
   */
  constructor(private router: Router, private invoiceService: InvoiceService, private dialog: MatDialog) {
  }

  ngOnInit(){
    this.invoiceService.init();

    this.invoiceService.emmiter.on('newInvoice', (invoice, type) => {
      if(type === 0)
        this.newInvoiceNotification(invoice, type);
    });
  }

  private newInvoiceNotification(invoice, type) {
    this.dialog.open(NewInvoiceDialogComponent, {
      maxWidth: '70%',
      data: {
        invoice
      }
    });
  }

  async ngAfterContentChecked(){
    this.showMenu = await this.showMenuEvent()
  }

  async showMenuEvent(): Promise<boolean> {
    return new Promise((s, f) => {
      this.router.events.pipe(filter(ev => ev instanceof NavigationEnd)).subscribe((event: NavigationEnd) => {
        s(event.url.indexOf('login') < 0 && event.url !== '');
      });
    })
  }
}
