import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import {MaterialModule} from './angularmaterials.module'

import { HttpClientModule }    from '@angular/common/http';


import { LandingPageComponent } from './landingPage/landing-page/landing-page.component';

import { RouterModule, Routes } from '@angular/router';
import {LandingPageModule}from './landingPage/landing-page.module';
import {LoginModule} from './login/login.module'

import {AuthGuard} from './login/_guards/auth.guard'
import {AuthenticationService} from './login/_services/authentication.service'
import { UserService} from './landingPage/_services/userService'

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './login/_interceptors/authenticationInterceptor';

const appRoutes: Routes = [
  { path: '',   redirectTo: '/landing-page', pathMatch: 'full' }
];


@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
  ],
  imports: [ 
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MaterialModule,
    LandingPageModule,
    LoginModule,
    RouterModule.forRoot(
      appRoutes,
      {enableTracing: true}
    )
  ],
  providers: [AuthGuard,
    AuthenticationService,
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
