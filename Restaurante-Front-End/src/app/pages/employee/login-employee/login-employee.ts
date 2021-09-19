import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

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

    authentication() {
        
        this.form
    }
}

