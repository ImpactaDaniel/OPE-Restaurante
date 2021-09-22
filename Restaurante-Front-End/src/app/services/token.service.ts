import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { APIResponse } from '../models/common/apiResponse';
@Injectable({
  providedIn: 'root'
})
export class TokenService {

  chaveToken = btoa('tokenRequest');
  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private url: string) { }

  public isAuthenticated(): boolean {
    let token = this.getToken();
    return token && token !== null;
  }

  private saveToken(token: TokenRespose): void {
    let json = JSON.stringify(token);
    localStorage.setItem(this.chaveToken, btoa(json));
  }

  public async renewToken(): Promise<boolean> {
    let response = await this.httpClient.get<APIResponse<any>>(this.url + 'Auth/RenewToken').toPromise();
    if (response.success)
      this.saveToken(response.response.result);
    return response.success;
  }

  public async authenticate(login: LoginModel): Promise<APIResponse<any>> {
    let response = await this.httpClient.post<APIResponse<any>>(this.url + 'Auth/Authenticate', login).toPromise();

    if (response.success)
      this.saveToken(response.response.result);

    return response;
  }

  public getToken(): TokenRespose {
    let json = this.returnFromLocalStorage(this.chaveToken);
    if (json === '' || !json)
      return null;
    return JSON.parse(json);
  }

  private returnFromLocalStorage(key: string): string {
    let value = localStorage.getItem(key);
    if (!value || value === '')
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
