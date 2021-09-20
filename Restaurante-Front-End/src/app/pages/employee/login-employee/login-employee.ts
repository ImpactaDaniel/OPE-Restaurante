import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoginModel, TokenService } from 'src/app/services/token.service';

@Component({
    selector: 'app-login-employee',
    templateUrl: './login-employee.html',
    styleUrls: ['./login-employee.css']
})

export class LoginEmployeeComponent {
    form: FormGroup;

    constructor(private formbuilder: FormBuilder, private tokenservice: TokenService) { }

    ngOnInit(): void {
        this.form = this.formbuilder.group({
            login: [""],
            password: [""]
        })
    }

    getInfoLogin(): LoginModel {
        let loginmodel = new LoginModel();
        loginmodel.email = this.form.get('login').value;
        loginmodel.password = this.form.get('password').value;
        return loginmodel;
    }
    
    authentication() {
        let loginmodel = this.getInfoLogin();
        let retorno = this.tokenservice.authenticate(loginmodel).toPromise()
    }
}

