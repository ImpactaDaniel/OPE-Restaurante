import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-employee-dialog',
  templateUrl: './employee-dialog.component.html',
  styleUrls: ['./employee-dialog.component.css']
})
export class DialogEmployeeComponent implements OnInit {

  public employee: any;
  public expandedElement: any;
  public employeesType = [
    {id: 0, name: 'Gerente'}, {id: 1, name: 'Funcionário Padrão'}, {id: 2, name: 'Entregador'}
  ]
  public phonesList: any;

  constructor(@Inject(MAT_DIALOG_DATA) private data: any, @Inject('BASE_URL') public url: string) { }

  ngOnInit(): void {
    this.employee = this.data?.employee;
    this.phonesList = this.data?.employee?.phones
  }

}
