import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import {AuthenticationService} from '../_services/authentication.service'

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  loading = false;
  username: string;
  password: string;
  hide: boolean = true;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit() {
    this.authenticationService.logout();
  }

  onSubmit() {
    this.loading = true;
    this.authenticationService.login(this.username, this.password)
    .subscribe(result => {
      if(result === true) {
        this.router.navigate(['/']);
      }
      else
      {
        this.loading = false;
      }
    })

  }

}
