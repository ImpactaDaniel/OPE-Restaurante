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
      name: ["", Validators.required],
      lastname: ["", Validators.required],
      email: ["", [Validators.required, Validators.email]],
      password: ["", [Validators.required, Validators.pattern('/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,}$/')]],
      street: ["", Validators.required],
      number: ["", Validators.required],
      district: ["", Validators.required],
      cep: ["", [Validators.minLength(8), Validators.pattern('^[0-9]{8}$')]],
      city: ["", Validators.required],
      state: ["", Validators.required],
      ddd: ["", [Validators.required, Validators.pattern(/\d+/g), Validators.maxLength(2)]],
      phoneNumber: ["", [Validators.required, Validators.pattern(/\d+/g), Validators.maxLength(9)]],
      bankId: [""],
      branch: ["", Validators.required],
      accountNumber: ["", [Validators.required, Validators.pattern(/\d+/g)]],
      digit: ["", [Validators.required, Validators.pattern(/\d+/g)]],
    })
  }


  async consultarCep() {
    let cep = this.form.get('cep');
    if (cep.invalid)
      return;
    var endereco = await this.consultaCepService.consultaCep(cep.value).toPromise();
    this.form.get('street').setValue(endereco.logradouro);
    this.form.get('state').setValue(endereco.uf);
    this.form.get('city').setValue(endereco.localidade);
    this.form.get('district').setValue(endereco.bairro);

  }

  async cadastrarFuncionario() {
    this.funcionario = this.getEmployee();

    let retorno = await this.employeeService.createEmployee(this.funcionario).toPromise();

    console.log(retorno);
  }

  private getEmployee(): Employee {
    let employee = new Employee()
    var account = new Account();
    account.accountNumber = this.form.get('accountNumber').value;
    account.digit = this.form.get('digit').value;
    account.branch = this.form.get('branch').value;
    account.bank = new Bank();
    account.bank.bankId = this.form.get('bankId').value;
    employee.account = account;
    var phone = new Phone();
    phone.phoneNumber = this.form.get('phoneNumber').value;
    phone.ddd = this.form.get('ddd').value;
    employee.phones.push(phone);
    let addres = new Address({
      street: this.form.get('street').value,
      number: this.form.get('number').value,
      district: this.form.get('district').value,
      cep: this.form.get('cep').value
    });
    employee.address = addres;
    employee.name = this.form.get('name').value;
    employee.lastName = this.form.get('lastname').value;
    employee.email = this.form.get('email').value;
    employee.password = this.form.get('password').value;

    return employee;
  }
}

