import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'

import { LandingPageRoutingModule } from './landing-page.routing.module'

import { MaterialModule } from '../angularmaterials.module'

import { DashboardComponent } from '../screens/dashboard/dashboard.component';
import { TimeSheetComponent } from '../screens/time-sheet/time-sheet.component';
import { TeamManagementComponent } from '../screens/team-management/team-management.component';

import { UserTimeWidgetComponent } from '../widgets/user-time-widget/user-time-widget.component';
import { ProjectViewerComponent } from '../widgets/project-viewer/project-viewer.component';

import { UserService } from './_services/userService'
import { TeamService } from './_services/teamService'

import { ChartsModule } from 'ng2-charts/ng2-charts';
import { AddNewTeamDialogComponent } from '../screens/team-management/add-new-team-dialog/add-new-team-dialog.component';
import { EditTeamDialogComponent } from '../screens/team-management/edit-team-dialog/edit-team-dialog.component';
import { ProjectService } from './_services/projectService';
import { ProjectManagementModule } from '../screens/project-management/project-management.module';


@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    LandingPageRoutingModule,
    ProjectManagementModule,
    ChartsModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [
    DashboardComponent,
    UserTimeWidgetComponent,
    ProjectViewerComponent,
    TimeSheetComponent,
    TeamManagementComponent,
    AddNewTeamDialogComponent,
    EditTeamDialogComponent
  ],
  providers: [UserService,
    TeamService,
    ProjectService
  ],
  entryComponents: [AddNewTeamDialogComponent, EditTeamDialogComponent]
})
export class LandingPageModule { }