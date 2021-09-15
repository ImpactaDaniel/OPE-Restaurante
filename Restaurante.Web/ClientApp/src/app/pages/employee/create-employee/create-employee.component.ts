import { HttpClient } from '@angular/common/http';
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

  private urlViaCep = "https://viacep.com.br/ws/";
  funcionario: Employee;
  form: FormGroup;

  constructor(private formbuilder: FormBuilder, private employeeService: EmployeeService, private httpClient: HttpClient) { }

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


  async consultarCep() {
    let cep = this.form.get('cep').value;
    console.log(`${this.urlViaCep}/${cep}/json`);
    
    var endereco = await this.httpClient.get<ViaCepResponse>(`${this.urlViaCep}/${cep}/json`).toPromise();
    this.form.get('street').setValue(endereco.logradouro);
    this.form.get('state').setValue(endereco.uf);
    this.form.get('city').setValue(endereco.localidade);
    this.form.get('district').setValue(endereco.bairro);

  }

  async cadastrarFuncionario() {
    this.funcionario = new Employee();
    var account = new Account();
    account.accountNumber = this.form.get('accountNumber').value;
    account.digit = this.form.get('digit').value;
    account.bank = new Bank();
    account.bank.bankId = this.form.get('bankId').value;
    this.funcionario.accounts.push(account);
    //var phone = new Phone();
    //this.funcionario.phones = this.form.get('phoneNumber').value;
    this.funcionario.name = this.form.get('name').value;
    this.funcionario.lastName = this.form.get('lastname').value;
    this.funcionario.email = this.form.get('email').value;
    this.funcionario.password = this.form.get('password').value;
    let retorno = await this.employeeService.createEmployee(this.funcionario).toPromise();
    console.log(retorno)
  }

}

class ViaCepResponse {
  logradouro: string;
  bairro: string;
  localidade: string;
  uf: string;
}
