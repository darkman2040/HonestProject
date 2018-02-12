import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { MediaMatcher } from '@angular/cdk/layout';
import { UserService } from '../_services/userService'
import { User } from '../models/User'

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  title = 'Honest Project';
  mobileQuery: MediaQueryList;
  loading: boolean;
  user: User;


  fillerNav = [`Dashboard`, `Timesheet`];

  private _mobileQueryListener: () => void;

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }

  constructor(changeDetectorRef: ChangeDetectorRef,
    media: MediaMatcher,
    private userService: UserService,
    private router: Router) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnInit() {
    this.loading = true;
    this.userService.GetCurrentUser()
      .subscribe(
        (user: User) => {
          this.user = user;
          console.log('User:' + JSON.stringify(user));
          this.loading = false;
        });
  }

  onSignOut() {
    this.router.navigate(['/login']);
  }

}
