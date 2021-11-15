import { Router } from '@angular/router';
import { TokenService } from './token.service';
import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RouteGuardService implements CanActivate {

constructor(private authService: TokenService, private router: Router) { }

canActivate(): boolean {
  if(!this.authService.isAuthenticated())
    this.router.navigate(['/employee/login']);

  return this.authService.isAuthenticated();
}

}
