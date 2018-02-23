import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialog, MatDialogRef } from '@angular/material';
import { TeamService } from '../../landingPage/_services/teamService'
import { Team } from '../../landingPage/models/Team'
import { TeamMember } from '../../landingPage/models/TeamMember'
import { AddNewTeamDialogComponent} from './add-new-team-dialog/add-new-team-dialog.component'
import { EditTeamDialogComponent } from './edit-team-dialog/edit-team-dialog.component';

@Component({
  selector: 'app-team-management',
  templateUrl: './team-management.component.html',
  styleUrls: ['./team-management.component.css']
})
export class TeamManagementComponent implements OnInit {

  displayedColumns = ['name', 'role'];
  teams: Team[];
  loading: boolean = true;
  dataSourceArray: Array<MatTableDataSource<TeamMember>>;

  constructor(private teamService: TeamService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.loading = true;
    this.loadTeams();
  }

  addNewTeam() {
    let dialogRef = this.dialog.open(AddNewTeamDialogComponent);
    dialogRef.afterClosed()
    .subscribe(result => {
      this.loadTeams();
    })
  }

  loadTeams() {
    this.teamService.GetManagedTeams()
      .subscribe(
        (teamList: Team[]) => {
          this.teams = teamList;
          console.log(teamList);
          this.dataSourceArray = new Array<MatTableDataSource<TeamMember>>();
          for (let i = 0; i < this.teams.length; i++) {
            let team = this.teams[i];
            this.dataSourceArray.push(new MatTableDataSource<TeamMember>(team.teamMembers)) 
          }
          this.loading = false;
        }
      )
  }

  onEdit(event : any, team: Team){
    event.stopPropagation();
    let dialogRef = this.dialog.open(EditTeamDialogComponent,
    {
      data: {team: team}
    });
    dialogRef.afterClosed()
    .subscribe(result => {
      this.loadTeams();
    })
  }

}