import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert.service';
import { DialogEmployeeComponent } from '../dialog-employee/employee-dialog.component';
import { EmployeeService } from '../service/employee.service';

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
  public searchField = "employeeName";
  public searchValue: string;

  public employeesType = [
    {id: 0, name: 'Gerente'}, {id: 1, name: 'Funcionário Padrão'}, {id: 2, name: 'Entregador'}
  ]
  
  public displayedColumns = ['employeeId', 'employeeName', 'employeeEmail', 'employeeType', 'employeeCreateDate', 'employeeBirthDate', 'remove', 'edit', 'details']

  constructor(
    private employeeService: EmployeeService,
    private dialog: MatDialog,
    private alertService: AlertService,
    private router: Router
    ) { }

  ngOnInit() {
    this.getEmployeesList();
  }

  private getEmployeesList() {

    if (this.isSearching) {
        this.employeeService.searchEmployees(this.searchField, this.searchValue, this.page, this.limit).subscribe(res => {
        console.log(res)
        this.employees.data = res.response.result?.entities;
        this.listSize = res.response.result?.size;
      });
      return;
    }
    this.employeeService.getAllEmployees(this.page, this.limit).subscribe(res => {
      this.employees.data = res.response.result.entities;
      this.listSize = res.response.result.size;
    })
  }

  public changePaginator(event: any) {
    this.limit = event.pageSize;
    this.page = event.pageIndex;
    this.getEmployeesList();
  }

  public search(event: any){
    this.searchValue = !isNaN(event.value) ? event.value : event.target.value;

    this.isSearching = true;
    this.page = 0;
    this.limit = 5;
    this.getEmployeesList();
  }

  public getStatusChoice(status: any) {
    this.statusChoice = status
  }

  public async details(id: number) {
    // let response = await this.employeeService.getInvoiceById(id).toPromise()
    // let invoice = response.response.result
    this.dialog.open(DialogEmployeeComponent, {
      maxWidth: '100%',
      data: {
        // invoice
      }
    });
  }

  public searchFieldChanged() {
    this.searchValue = "";
  }

  public removeFilters() {
    this.isSearching = false;
    this.searchField = "employeeName";
    this.searchValue = "";
    this.getEmployeesList();
  }

  public remove(id: number) {
  }

}
