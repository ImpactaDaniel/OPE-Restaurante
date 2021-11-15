import { AlertService } from './../../../services/alert.service';
import { TokenService } from './../../../services/token.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ChangePasswordModel } from 'src/app/models/common/change-password.model';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  public fg: FormGroup;
  public error = false;
  public erroMsg = "";

  constructor(private fb: FormBuilder, private authService: TokenService, private alertService: AlertService) { }

  ngOnInit() {
    this.buildForm();
  }

  private buildForm() {
    this.fg = this.fb.group({
      oldPassword: ['', [Validators.required, Validators.pattern(/(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])/)]],
      newPassword: ['', [Validators.required, Validators.pattern(/(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])/)]],
      repeatNewPassword: ['', [Validators.required, Validators.pattern(/(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])/)]]
    });
  }

  public changePassword() {
    if (this.fg.invalid) return;

    this.authService.updatePassowrd(new ChangePasswordModel(this.fg.value)).then(res => {
      if (res.success) {
        this.alertService.showSuccess("Senha Alterada!", "Senha alterada com sucesso!");
        return;
      }

      this.error = true;
      this.erroMsg = "Senha atual inválida!";
    });
  }

  public mathConfirm(type1: any, type2: any) {
    let value1 = this.fg.controls[type1];
    let value2 = this.fg.controls[type2];

    if (!value2.touched) return;

    if (value1.value === value2.value) {
      value2.setErrors(null);
      return;
    }

    return value2.setErrors({ notEquivalent: true });
  }

}
