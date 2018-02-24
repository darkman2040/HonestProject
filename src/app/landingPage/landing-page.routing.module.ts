import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LandingPageComponent } from './landing-page/landing-page.component';
import { DashboardComponent } from '../screens/dashboard/dashboard.component';
import { TimeSheetComponent } from '../screens/time-sheet/time-sheet.component';
import { TeamManagementComponent } from '../screens/team-management/team-management.component'

import { AuthGuard } from '../login/_guards/auth.guard'
import { ProjectManagementComponent } from '../screens/project-managerment/project-management.component';

const screensRoutes: Routes = [
  {
    path: 'landing-page',
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
      },
      {
        path: 'team-management',
        component: TeamManagementComponent,
      },
      {
        path: 'project-management',
        component: ProjectManagementComponent,
      }
      

    ],
    canActivate: [AuthGuard]
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