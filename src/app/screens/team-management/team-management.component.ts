import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from '@angular/material';
import {TeamService} from '../../landingPage/_services/teamService'
import {Team} from '../../landingPage/models/Team'

@Component({
  selector: 'app-team-management',
  templateUrl: './team-management.component.html',
  styleUrls: ['./team-management.component.css']
})
export class TeamManagementComponent implements OnInit {

  displayedColumns = ['name', 'role'];
  teams: Team[];
  loading: boolean = true;
  dataSource1 = new MatTableDataSource(Team1);
  dataSource2 = new MatTableDataSource(Team2);
  dataSourceArray: MatTableDataSource<any>[] = [this.dataSource1, this.dataSource2];

  constructor(private teamService: TeamService) { }

  ngOnInit() {
    this.loading = true;
    this.teamService.GetManagedTeams()
    .subscribe(
      (teamList: Team[]) =>
      {
        this.teams = teamList;
        console.log("Teams: " + JSON.stringify(teamList));
        this.loading = false;
      }
    )
  }

}

const Team1: any[] = [
  {name: 'Colin Gormley', role: 'Team Member'},
  {name: 'Osama LongName', role: 'Team Member'}
];

const Team2: any[] = [
  {name: 'Eric Lavangi', role: 'Team Member'},
  {name: 'Osama LongName', role: 'Team Member'}
];
