import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import {MaterialModule} from './angularmaterials.module'

import {HttpClientModule} from '@angular/common/http';
import {CdkTableModule} from '@angular/cdk/table';

import { ChartsModule } from 'ng2-charts/ng2-charts';

import {UserTimeWidgetComponent} from './widgets/user-time-widget/user-time-widget.component';
import { DevDashboardComponent } from './screens/dev-dashboard/dev-dashboard.component';


@NgModule({
  declarations: [
    AppComponent,
    UserTimeWidgetComponent,
    DevDashboardComponent
  ],
  imports: [
    CdkTableModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MaterialModule,
    ChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
