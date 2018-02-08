import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import {MaterialModule} from './angularmaterials.module'

import { HttpClientModule }    from '@angular/common/http';


import { LandingPageComponent } from './landingPage/landing-page/landing-page.component';
import { LoginPageComponent } from './login/loginPage/login-page/login-page.component';

import { RouterModule, Routes } from '@angular/router';
import {LandingPageModule}from './landingPage/landing-page.module';

import {AuthGuard} from './login/_guards/auth.guard'
import {AuthenticationService} from './login/_services/authentication.service'
import { UserService} from './landingPage/_services/userService'

const appRoutes: Routes = [
  { path: 'login', component: LoginPageComponent },
  { path: '',   redirectTo: '/landing-page', pathMatch: 'full' }
];


@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    LoginPageComponent,
  ],
  imports: [ 
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    LandingPageModule,
    RouterModule.forRoot(
      appRoutes,
      {enableTracing: true}
    )
  ],
  providers: [AuthGuard,
    AuthenticationService,
    UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
