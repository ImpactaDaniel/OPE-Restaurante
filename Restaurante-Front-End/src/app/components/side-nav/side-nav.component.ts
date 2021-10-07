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

  public logout() {
    this.authService.logout();
    this.router.navigate(['/employee/login']);
    this.onSidenavClose();
  }

}