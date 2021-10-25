import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {

  @Output() sidenavClose = new EventEmitter();

  constructor(private authService: TokenService, private router: Router) { }

  ngOnInit() {
  }

  public onSidenavClose = () => {
    this.sidenavClose.emit()
  }

  public toBanks(): void {
    this.router.navigate(['bank/list'])
    this.onSidenavClose()
  }

  public toEmployees(): void {
    this.router.navigate(['employee/create'])
    this.onSidenavClose()
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
