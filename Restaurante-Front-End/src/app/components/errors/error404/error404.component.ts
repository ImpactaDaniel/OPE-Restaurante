import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
  templateUrl: './error404.component.html'
})

export class Error404Component {

  constructor(private router: Router) { }

  redirect() {
    console.log('tete')
    this.router.navigate(['/employee/create'])
  }

}
