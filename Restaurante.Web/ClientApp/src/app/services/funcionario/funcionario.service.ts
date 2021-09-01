import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FuncionarioService {

  constructor(@Inject('BASE_URL') private url: string, private httpClient: HttpClient) { }

  createUser() {
    return this.httpClient.post(this.url + 'Funcionarios/CreateNew', {
      name: "Daniel",
      Email: "daniel@gmail.com"
    });
  }
}
