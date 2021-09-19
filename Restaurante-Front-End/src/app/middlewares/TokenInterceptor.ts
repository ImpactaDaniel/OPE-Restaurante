import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { TokenService } from "../services/token.service";

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

    constructor(private tokenService: TokenService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(!this.tokenService.isAuthenticated() || req.url.includes('viacep.com.br/ws/'))
            return next.handle(req);
            
        let tokenResponse = this.tokenService.getToken();
        let newReq = req.clone({
            setHeaders: {
                Authorization: `Bearer ${tokenResponse.token}`
            }
        });
        return next.handle(newReq);
    }
}