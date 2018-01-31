import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LandingPageComponent }from './landing-page/landing-page.component';
import { DashboardComponent }    from '../screens/dashboard/dashboard.component';
import {TimeSheetComponent} from '../screens/time-sheet/time-sheet.component';

const screensRoutes: Routes = [
  { path: 'landing-page',  
  component: LandingPageComponent, 
  children: [
    {
      path: '',
      component: DashboardComponent
    },
    {
      path: 'dashboard',
      component: DashboardComponent
    },
    {
      path: 'time-sheet',
      component: TimeSheetComponent
    }
    
  ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(screensRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class LandingPageRoutingModule { }