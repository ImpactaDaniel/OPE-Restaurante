import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Employee } from 'src/app/models/funcionario/funcionario';
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
      nome: [""],
      sobrenome: [""],
      email: [""],
      senha: [""],
      endereco: [""]
    })
  }

  async cadastrarFuncionario() {
    this.funcionario = new Employee();
    this.funcionario.name = this.form.get('nome').value;
    this.funcionario.lastName = this.form.get('sobrenome').value;
    this.funcionario.email = this.form.get('email').value;
    this.funcionario.password = this.form.get('senha').value;
    let retorno = await this.employeeService.createEmployee(this.funcionario).toPromise();
    console.log(retorno)
  }

}
