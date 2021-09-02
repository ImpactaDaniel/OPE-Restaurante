import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cadastro-funcionario',
  templateUrl: './cadastro-funcionario.component.html',
  styleUrls: ['./cadastro-funcionario.component.css']
})
export class CadastroFuncionarioComponent implements OnInit {
  funcionario: Funcionario;
  form: FormGroup;
  async cadastrarFuncionario() {
    this.funcionario = new Funcionario();
    this.funcionario.nome = this.form.get('nome').value;
    this.funcionario.sobrenome = this.form.get('sobrenome').value;
    this.funcionario.email = this.form.get('email').value;
    this.funcionario.senha = this.form.get('senha').value;
    this.funcionario.endereco = this.form.get('endereco').value;
    let retorno = await this.chamarCadastroFuncionario(this.funcionario).toPromise();
    console.log(retorno)
  }
  private chamarCadastroFuncionario(funcionario: Funcionario): Observable<any> {
    return this.httpclient.post<any>(this.urlbase + 'Funcionarios/CreateNew', funcionario)
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
  nome: string;
  sobrenome: string;
  email: string;
  senha: string;
  endereco: string;
}
