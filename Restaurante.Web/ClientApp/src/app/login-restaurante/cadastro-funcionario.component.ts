import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  templateUrl: './cadastro-funcionario.component.html',
  styleUrls: ['./cadastro-funcionario.component.css']
})
export class LoginFuncionario implements OnInit {
  funcionario: Funcionario;
  form: FormGroup;
  async cadastrarFuncionario() {
    this.funcionario = new Funcionario();
    this.funcionario.name = this.form.get('nome').value;
    this.funcionario.lastName = this.form.get('sobrenome').value;
    this.funcionario.email = this.form.get('email').value;
    this.funcionario.password = this.form.get('senha').value;
    this.funcionario.address = this.form.get('endereco').value;
    let retorno = await this.chamarCadastroFuncionario(this.funcionario).toPromise();
    console.log(retorno)
  }
  private chamarCadastroFuncionario(funcionario: Funcionario): Observable<any> {
    return this.httpclient.post<any>(this.urlbase + 'Funcionarios/Authenticate', funcionario)
  }

  constructor(private formbuilder: FormBuilder, private httpclient: HttpClient, @Inject('BASE_URL') private urlbase: string) { }

  ngOnInit(): void {
    this.form = this.formbuilder.group({
      nome: [""],
      sobrenome: [""],
      email: [""],
      senha: [""],
      endereco: [""]
    })
  }

}

export class Funcionario {
  name: string;
  lastName: string;
  email: string;
  password: string;
  address: string;
}
