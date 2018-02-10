import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component'
import { RegisterPageComponent } from './register-page/register-page.component';

const loginRoutes: Routes = [
    { path: 'login', component: LoginPageComponent },
    { path: 'register', component: RegisterPageComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(loginRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class LoginRoutingModule { }

