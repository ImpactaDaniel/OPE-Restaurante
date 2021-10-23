import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";
import { TokenService } from "src/app/services/token.service";

@Injectable({ providedIn: 'root' })
export class EmployeeGuard implements CanActivate{

    constructor(private tokenService: TokenService, private router: Router) {}
    
    canActivate(): boolean {
        if(!this.tokenService.isAuthenticated()){
            this.router.navigate(['/employee/login']);  
            return false
        }
        return true
    }
}