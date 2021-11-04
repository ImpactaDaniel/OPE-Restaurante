import { NewInvoiceDialogModule } from './components/dialogs/new-invoice-dialog/new-invoice-dialog.module';
import { GlobalErrorHandler } from './middlewares/GlobalErrorHandler';
import { Error404Component } from './components/errors/error404/error404.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RequestInterceptor } from './middlewares/TokenInterceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderModule } from './components/header/header.module';
import { SideNavModule } from './components/side-nav/side-nav.module';
import { ErrorHandler, NgModule } from '@angular/core';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot([
      {
        path: 'employee',
        loadChildren: () => import('./pages/employee/employee.module').then(m => m.EmployeeModule)
      },{
        path: 'deliveryman',
        loadChildren: () => import('./pages/deliveryman/deliveryman.module').then(m => m.DeliverymanModule)
      },
      {
        path: 'bank',
        loadChildren: () => import('./pages/bank/bank.module').then(m => m.BankModule)
      },
      {
        path: 'invoice',
        loadChildren: () => import('./pages/invoice/invoice.module').then(m => m.InvoiceModule)
      },
      {
        path: 'products',
        loadChildren: () => import('./pages/products/products.module').then(m => m.ProductsModule)
      },
      {
        path: '**',
        component: Error404Component
      },
    ]),
    HttpClientModule,
    BrowserAnimationsModule,
    SideNavModule,
    HeaderModule,
    MatSidenavModule,
    MatDialogModule,
    NewInvoiceDialogModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
