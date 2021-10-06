import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
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

  constructor(private formbuilder: FormBuilder,
              private tokenservice: TokenService,
              private router: Router,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.form = this.formbuilder.group({
      email: [""],
      password: [""]
    })
  }

  getInfoLogin(): LoginModel {
    let loginmodel = new LoginModel();
    loginmodel.email = this.form.get('email').value;
    loginmodel.password = this.form.get('password').value;
    return loginmodel;
  }

  authentication() {
    let loginmodel = this.getInfoLogin();
    this.tokenservice.authenticate(loginmodel).then((response) => {
      if(!response.success)
      {
        var message = '';
        response.notifications.map(not => {
          message += not.message;
        });
        this.alertService.showError(null, message);
        return;
      }
      if(this.tokenservice.getTokenData().firstAccess === 'True')
        console.log('haa');
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
