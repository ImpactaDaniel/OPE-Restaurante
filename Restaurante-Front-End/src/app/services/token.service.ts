import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { APIResponse } from '../models/common/apiResponse';
@Injectable({
  providedIn: 'root'
})
export class TokenService {

  chaveToken = btoa('tokenRequest');
  constructor(private httpClient: HttpClient) { }

  public isAuthenticated(): boolean {
    let token = this.getToken();
    if(!token || token === null)
      return false;
    return new Date(token.validTo) > new Date();
  }

  private saveToken(token: TokenRespose): void {
    let json = JSON.stringify(token);
    localStorage.setItem(this.chaveToken, btoa(json));
  }

  public async authenticate(login: LoginModel, url: string): Promise<void> {
    let response = await this.httpClient.post<APIResponse<any>>(url, login).toPromise();

    if(response.success)
      this.saveToken(response.response.result);
  }

  public getToken(): TokenRespose {
    let json = this.returnFromLocalStorage(this.chaveToken); 
    console.log(json);
    if(json === '' || !json)
      return null;
    return JSON.parse(json);
  }

  private returnFromLocalStorage(key: string): string{
    let value = localStorage.getItem(key);
    if(!value || value === '')
      return value;
    return atob(value);
  }
}

export class TokenRespose {
  token: string;
  validTo: Date;
  validFrom: Date;
}

export class LoginModel {
  email: string;
  password: string;
}