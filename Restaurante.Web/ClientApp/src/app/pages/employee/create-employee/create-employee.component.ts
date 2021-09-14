import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Account, Bank, Employee, Phone } from '../../../models/funcionario/employee';
import { EmployeeService } from '../service/employee.service';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {

  funcionario: Employee;
  form: FormGroup;

  constructor(private formbuilder: FormBuilder, private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.form = this.formbuilder.group({
      name: [""],
      lastname: [""],
      email: [""],
      password: [""],
      street: [""],
      number: [""],
      district: [""],
      cep: [""],
      city: [""],
      state: [""],
      ddd: [""],
      phoneNumber: [""],
      bankId: [""],
      branch: [""],
      accountNumber: [""],
      digit: [""],
    })
  }

  async cadastrarFuncionario() {
    this.funcionario = new Employee();
    var account = new Account();
    this.funcionario.accounts = this.form.get('account').value;
    var bank = new Bank();
    this.funcionario.banks = this.form.get('bank').value;
    var phone = new Phone();
    this.funcionario.phones = this.form.get('phone').value;
    this.funcionario.name = this.form.get('name').value;
    this.funcionario.lastName = this.form.get('lastname').value;
    this.funcionario.email = this.form.get('email').value;
    this.funcionario.password = this.form.get('password').value;
    let retorno = await this.employeeService.createEmployee(this.funcionario).toPromise();
    console.log(retorno)
  }

}
