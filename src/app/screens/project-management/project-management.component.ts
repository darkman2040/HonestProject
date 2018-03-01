import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialog } from '@angular/material';
import { ProjectService } from '../../landingPage/_services/projectService';
import { Project } from '../../landingPage/models/Project';
import { TimePercentageUserProjectWorkType } from '../../landingPage/models/TimePercentageUserProjectWorkType';
import { ProjectWorkType } from '../../landingPage/models/ProjectWorkType';
import { AddProjectDialogComponent } from './add-project-dialog/add-project-dialog.component';

@Component({
  selector: 'app-project-management',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.css']
})
export class ProjectManagementComponent implements OnInit {

  displayedColumns = ['name', 'pct'];
  projects: ViewProject[];
  loading: boolean = true;
  teamId: string;

  constructor(
    public projectService: ProjectService,
    private dialog: MatDialog
  ) {

  };

  ngOnInit() {
    this.loadProjects();
  }

  loadProjects() {
    this.loading = true;
    this.projects = new Array<ViewProject>();
    this.projectService.GetProjects().subscribe(
      (projects: Project[]) => {
        projects.forEach((project: Project) => {
          if (projects) {
            this.teamId = projects[0].teamId;
          }
          let projWorkType = new Array<ViewProjectWorkType>();
          project.workTypes.forEach((workType: ProjectWorkType) => {
            projWorkType.push(new ViewProjectWorkType(workType.name, workType.manHours, new MatTableDataSource<TimePercentageUserProjectWorkType>(workType.userWorkList)));
          });

          this.projects.push(new ViewProject(project.name, project.description, projWorkType));
        });

        this.loading = false;
      })
  }

  addNewProject() {
    let dialogRef = this.dialog.open(AddProjectDialogComponent,
      {
        data: { teamId: this.teamId }
      });
    dialogRef.afterClosed()
      .subscribe(result => {
        this.loadProjects();
      })
  }

  onEdit(event: any, team: Project) {
    event.stopPropagation();
  }
}

export class ViewProjectWorkType {
  constructor(public name: string,
    public manHours: number,
    public userWorkList: MatTableDataSource<TimePercentageUserProjectWorkType>) { }
}

export class ViewProject {
  constructor(
    public name: string,
    public description: string,
    public workTypes: ViewProjectWorkType[]) { }
}
