import { Roles } from './../../models/common/token.data.model';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { TokenData } from 'src/app/models/common/token.data.model';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {

  @Output() sidenavClose = new EventEmitter();

  public user: TokenData;

  public eRoles: Roles;

  constructor(private authService: TokenService, private router: Router) { }

  ngOnInit() {
    this.getUser();
    this.authService.userChanged.subscribe(() => {
      this.getUser();
    });
  }

  private getUser() {
    if(this.authService.isAuthenticated())
      this.user = this.authService.getTokenData();
  }

  public onSidenavClose = () => {
    this.sidenavClose.emit()
  }

  public toBanks(): void {
    this.router.navigate(['bank/list'])
    this.onSidenavClose()
  }

  public toEmployees(): void {
    this.router.navigate(['employee/list'])
    this.onSidenavClose()
  }

  public toProducts(): void {
    this.router.navigate(['products/list'])
    this.onSidenavClose();
  }

  public toDelivers(): void {
    this.router.navigate(['deliveryman/create'])
    this.onSidenavClose()
  }

  public toInvoices(): void {
    this.router.navigate(['invoice/list'])
    this.onSidenavClose()
  }

  public logout() {
    this.authService.logout();
    this.router.navigate(['/employee/login']);
    this.onSidenavClose();
  }

}
