import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import {FormsModule} from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule }    from '@angular/common/http';
import {MaterialModule} from '../angularmaterials.module';
import {LoginRoutingModule} from './login.routing.module';
import {LoginPageComponent} from './login-page/login-page.component';
import {RegisterPageComponent} from './register-page/register-page.component';

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
        RegisterPageComponent
    ],
    providers: [ ]
  })
  export class LoginModule {}