import { Component, OnInit } from '@angular/core';
import { RegisterSite } from '../_models/registerSite.model'
import {Router} from '@angular/router'
import {SiteService} from '../_services/site.service'

@Component({
  selector: 'app-site-register',
  templateUrl: './site-register.component.html',
  styleUrls: ['./site-register.component.css']
})
export class SiteRegisterComponent implements OnInit {

  registerSite = new RegisterSite('', 8, false, '');
  loading = false;

  constructor(private router: Router,
    private siteService: SiteService) { }

  ngOnInit() {
  }

  onSubmit() {
    this.siteService.RegisterNewSite(this.registerSite)
    .subscribe(result => {
      if(result === true) {
        this.router.navigate(['/register']);
      }
      else
      {
        this.loading = false;
      }
    })
  }

}
