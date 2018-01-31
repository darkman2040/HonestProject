import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent }    from './dashboard/dashboard.component';
import { TimeSheetComponent }  from './timeSheet/time-sheet.component';

const screensRoutes: Routes = [
  { path: 'dashboard',  component: DashboardComponent },
  { path: 'timesheet', component: TimeSheetComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(screensRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class ScreensRoutingModule { }