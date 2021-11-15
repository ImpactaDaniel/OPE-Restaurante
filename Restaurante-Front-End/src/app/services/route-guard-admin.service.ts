import { TokenService } from './token.service';
import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Roles } from '../models/common/token.data.model';

@Injectable({
  providedIn: 'root'
})
export class RouteGuardAdminService implements CanActivate {

  constructor(private authService: TokenService, private router: Router) { }

  canActivate(): boolean {
    if(!this.authService.isAuthenticated() || this.authService.getTokenData().role !== Roles.MANAGER)
      this.router.navigate(['/unauthorized']);

    return this.authService.isAuthenticated();
  }

}
