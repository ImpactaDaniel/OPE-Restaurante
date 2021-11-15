import { RouteGuardAdminService } from './../../../services/route-guard-admin.service';
import { LogoModule } from './../../../components/logo/logo.module';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from "@angular/core";
import { CreateComponent } from "./create.component";
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: CreateComponent,
    canActivate: [RouteGuardAdminService]
  }
]

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    RouterModule.forChild(routes),
    LogoModule
  ],
  declarations: [CreateComponent]
})
export class CreateBankModule {

}
