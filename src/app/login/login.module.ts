import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from '../angularmaterials.module';

import { LoginRoutingModule } from './login.routing.module';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { SiteRegisterComponent } from './site-register/site-register.component'
import { SiteService } from './_services/site.service'
import {RegisterUserService} from './_services/registerUser.service'
import {SiteRegisterGuard} from './_guards/siteRegister.guard'

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    HttpClientModule,
    LoginRoutingModule,
    ReactiveFormsModule
  ],
  declarations: [
    LoginPageComponent,
    RegisterPageComponent,
    SiteRegisterComponent
  ],
  providers: [SiteService,
    RegisterUserService,
    SiteRegisterGuard]
})
export class LoginModule { }