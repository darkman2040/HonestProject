import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-project-managerment',
  templateUrl: './project-management.component.html',
  styleUrls: ['./project-management.component.css']
})
export class ProjectManagementComponent implements OnInit {

  displayedColumns = ['name', 'pct'];
  projects: Project[];
  loading: boolean = true;
  dataSourceArray: Array<MatTableDataSource<TimePercentageUserProjectWorkType>>;

  constructor() {

    let timePctWorkTypes = [
      new TimePercentageUserProjectWorkType("Colin", "Gormley", 25),
      new TimePercentageUserProjectWorkType("Eric", "Lavangi", 25),
      new TimePercentageUserProjectWorkType("Osama", "AbdullaHussein", 25),
      new TimePercentageUserProjectWorkType("Kevin", "Weltch", 25)
    ]

    let workTypes = [
      new ProjectWorkType("Planning", 35, new MatTableDataSource<TimePercentageUserProjectWorkType>(timePctWorkTypes)),
      new ProjectWorkType("Development", 35, new MatTableDataSource<TimePercentageUserProjectWorkType>(timePctWorkTypes)),
      new ProjectWorkType("Testing", 25, new MatTableDataSource<TimePercentageUserProjectWorkType>(timePctWorkTypes)),
      new ProjectWorkType("Deployment", 5, new MatTableDataSource<TimePercentageUserProjectWorkType>(timePctWorkTypes))
    ]
    this.projects = [
      new Project("Adv. Search Web", "Dumpster fire PR project", workTypes),
      new Project("Word Converter", "Upgrade Word renditions to this decade", workTypes)
    ]
  };

  ngOnInit() {
    this.loading = false;
  }

}

export class TimePercentageUserProjectWorkType {
  constructor(public firstName: string,
    public lastName: string,
  public workPct: number) {}
}

export class ProjectWorkType {
  constructor(public name: string,
    public workPct: number,
  public userWorkList: MatTableDataSource<TimePercentageUserProjectWorkType>) { }
}

export class Project {
  constructor(
    public name: string,
    public description: string,
    public workTypes: ProjectWorkType[]) { }
}
