import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { EmployeeService } from '../../service/employee.service';

@Component({
  selector: 'app-list-employee',
  templateUrl: './list-employee.component.html',
  styleUrls: ['./list-employee.component.css']
})
export class ListEmployeeComponent implements OnInit {

  public employees = new MatTableDataSource<any>();

  public statusChoice: string;
  public limit: number = 5;
  public page: number = 0;
  public listSize: number = 0;
  private isSearching = false;
  public status: string;
  public searchField = "customerName";
  public searchValue: string;

  // public invoiceStatusDescription = [
  //   {id: 0, name: 'Criado'}, {id: 1, name: 'Aceito'}, {id: 2, name: 'Rejeitado'}, {id: 3, name: 'Pagamento Pendente'},
  //   {id: 4, name: 'Pago'}, {id: 5, name: 'Enviado'}, {id: 6, name: 'Entregue'}, {id: 7, name: 'Fechado'}
  // ]
  // public paymentTypeDescription = ['Débito', 'Crédito']

  public displayedColumns = ['employeeId', 'employeeName', 'employeeEmail', 'employeePhone', 'employeeCreateDate', 'employeeBirthDate', 'remove', 'edit', 'details']

  constructor(
    private employeeService: EmployeeService,
    private alertService: AlertService,
    private router: Router
    ) { }

  ngOnInit() {
    this.getEmployeesList();
  }

  private getEmployeesList() {

    this.employeeService.getAllInvoices().subscribe(res => {
      console.log(res)
      this.employees.data = res.response;
      this.listSize = res.response.size;
    })
  }

  public changePaginator(event: any) {
    this.limit = event.pageSize;
    this.page = event.pageIndex;
    this.getEmployeesList();
  }

  // public search(event: any){
  //   this.searchValue = !isNaN(event.value) ? event.value : event.target.value;

  //   this.isSearching = true;
  //   this.page = 0;
  //   this.limit = 5;
  //   this.getEmployeesList();
  // }

  public getStatusChoice(status: any) {
    this.statusChoice = status
  }

  public async details(id: number) {
    // let response = await this.invoiceService.getInvoiceById(id).toPromise()
    // let invoice = response.response.result
    // this.dialog.open(InvoiceDetailsDialogComponent, {
    //   maxWidth: '100%',
    //   data: {
    //     invoice
    //   }
    // });
  }

  public searchFieldChanged() {
    this.searchValue = "";
  }

  public removeFilters() {
    this.isSearching = false;
    this.searchField = "customerName";
    this.searchValue = "";
    this.getEmployeesList();
  }

  public remove(id: number) {
  }

}
