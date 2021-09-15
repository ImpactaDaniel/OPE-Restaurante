import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConsultaCepService } from 'src/app/services/consulta-cep.service';
import { Account, Address, Bank, Employee, Phone } from '../../../models/funcionario/employee';
import { EmployeeService } from '../service/employee.service';

@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})
export class CreateEmployeeComponent implements OnInit {


  funcionario: Employee;
  form: FormGroup;

  constructor(private formbuilder: FormBuilder, private employeeService: EmployeeService, private consultaCepService: ConsultaCepService) { }

  ngOnInit(): void {
    this.form = this.formbuilder.group({
      name: [""],
      lastname: [""],
      email: [""],
      password: [""],
      street: [""],
      number: [""],
      district: [""],
      cep: ["", [Validators.minLength(8), Validators.pattern('^[0-9]{8}$')]],
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
    let cep = this.form.get('cep');
    console.log(cep);
    if (cep.invalid)
      return;
    var endereco = await this.consultaCepService.consultaCep(cep.value).toPromise();
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
    account.branch = this.form.get('branch').value;
    account.bank = new Bank();
    account.bank.bankId = this.form.get('bankId').value;
    this.funcionario.account = account;
    var phone = new Phone();
    phone.phoneNumber = this.form.get('phoneNumber').value;
    phone.ddd = this.form.get('ddd').value;
    this.funcionario.phones.push(phone);
    let addres = new Address({
      street: this.form.get('street').value,
    });
    this.funcionario.address = addres;
    this.funcionario.name = this.form.get('name').value;
    this.funcionario.lastName = this.form.get('lastname').value;
    this.funcionario.email = this.form.get('email').value;
    this.funcionario.password = this.form.get('password').value;
    let retorno = await this.employeeService.createEmployee(this.funcionario).toPromise();
  }

}

