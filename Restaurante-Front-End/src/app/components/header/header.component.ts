import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  @Output() public sidenavToggle = new EventEmitter();

  userNameLogged: string;

  constructor() { }

  ngOnInit() {

  }

  public onToggleSidenav = () => {
    this.sidenavToggle.emit()
  }

  logOut() {

  }
}
