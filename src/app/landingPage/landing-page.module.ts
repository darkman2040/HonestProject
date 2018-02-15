import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import {FormsModule} from '@angular/forms'

import {LandingPageRoutingModule} from './landing-page.routing.module'

import {MaterialModule} from '../angularmaterials.module'

import { DashboardComponent }    from '../screens/dashboard/dashboard.component';
import {TimeSheetComponent} from '../screens/time-sheet/time-sheet.component';
import {TeamManagementComponent} from '../screens/team-management/team-management.component';

import {UserTimeWidgetComponent} from '../widgets/user-time-widget/user-time-widget.component';
import { ProjectViewerComponent } from '../widgets/project-viewer/project-viewer.component';

import { UserService} from './_services/userService'
import { TeamService} from './_services/teamService'

import { ChartsModule } from 'ng2-charts/ng2-charts';


@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    LandingPageRoutingModule,
    ChartsModule,
    FormsModule
  ],
  declarations: [
    DashboardComponent,
    UserTimeWidgetComponent,
    ProjectViewerComponent,
    TimeSheetComponent,
    TeamManagementComponent
  ],
  providers: [UserService,
    TeamService ]
})
export class LandingPageModule {}