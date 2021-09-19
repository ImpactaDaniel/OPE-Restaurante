import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  public isAuthenticated(): boolean {
    let token = new TokenRespose();
    return true;
    return new Date() < new Date(token.validTo);
  }

  private saveToken(token: TokenRespose): void {
    
  }

  public authenticate(login: LoginModel): void {

  }

  public getToken(): TokenRespose {
    let token = new TokenRespose();
    token.token = "agadgdagad";
    return token;
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