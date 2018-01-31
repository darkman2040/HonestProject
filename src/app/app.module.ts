import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import {MaterialModule} from './angularmaterials.module'

import {HttpClientModule} from '@angular/common/http';


import { LandingPageComponent } from './landingPage/landing-page/landing-page.component';
import { LoginPageComponent } from './login/loginPage/login-page/login-page.component';

import { RouterModule, Routes } from '@angular/router';
import {ScreensModule}from './screens/screens.module';

import { DashboardComponent }    from './screens/dashboard/dashboard.component';
import { TimeSheetComponent }  from './screens/timeSheet/time-sheet.component';

import {UserTimeWidgetComponent} from './widgets/user-time-widget/user-time-widget.component';
import { ProjectViewerComponent } from './widgets/project-viewer/project-viewer.component';

import { ChartsModule } from 'ng2-charts/ng2-charts';

const appRoutes: Routes = [
  { path: 'landing', component: LandingPageComponent },
  { path: 'login', component: LoginPageComponent },
 
  { path: '',   redirectTo: '/landing', pathMatch: 'full' }
];


@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    LoginPageComponent,
    DashboardComponent,
    TimeSheetComponent,
    UserTimeWidgetComponent,
    ProjectViewerComponent
  ],
  imports: [ 
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule,
    MaterialModule,
    ScreensModule,
    ChartsModule,
    RouterModule.forRoot(
      appRoutes,
      {enableTracing: true}
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
