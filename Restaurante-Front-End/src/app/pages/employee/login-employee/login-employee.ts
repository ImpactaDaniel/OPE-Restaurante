import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/services/alert.service';
import { TokenService } from 'src/app/services/token.service';
import { LoginModel } from 'src/app/models/common/login.model';

@Component({
  selector: 'app-login-employee',
  templateUrl: './login-employee.html',
  styleUrls: ['./login-employee.css']
})

export class LoginEmployeeComponent {
  form: FormGroup;
  error: boolean;
  erroMsg = "";
  constructor(private formbuilder: FormBuilder,
              private tokenservice: TokenService,
              private router: Router,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.form = this.formbuilder.group({
      email: ["", [Validators.required]],
      password: ["", [Validators.required]]
    })
  }

  getInfoLogin(): LoginModel {
    let loginmodel = new LoginModel();
    loginmodel.email = this.form.get('email').value;
    loginmodel.password = this.form.get('password').value;
    return loginmodel;
  }

  authentication() {
    if(!this.form.valid) return;
    this.erroMsg = "";
    let loginmodel = this.getInfoLogin();
    this.tokenservice.authenticate(loginmodel).then((response) => {
      if(!response.success)
      {
        var message = '';
        response.notifications.map(not => {
          message += not.message;
        });
        this.erroMsg = message;
        this.error = true;
        return;
      }
      if(this.tokenservice.getTokenData().firstAccess === 'True')
      this.router.navigate(['/']);
    }).catch(e => {
      var message = '';
        e.error.notifications.map(not => {
          message += not.message;
        });
        this.alertService.showError(null, message);
    });
  }
}
