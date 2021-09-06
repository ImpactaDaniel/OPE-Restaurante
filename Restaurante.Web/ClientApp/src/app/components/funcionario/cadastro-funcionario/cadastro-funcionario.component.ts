import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Employee } from '../../../models/funcionario/funcionario';
import { EmployeeService } from '../../../services/funcionario/funcionario.service';

@Component({
  selector: 'app-cadastro-funcionario',
  templateUrl: './cadastro-funcionario.component.html',
  styleUrls: ['./cadastro-funcionario.component.css']
})
export class CadastroFuncionarioComponent implements OnInit {
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
