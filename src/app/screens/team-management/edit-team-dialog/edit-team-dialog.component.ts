import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { User } from '../../../landingPage/models/User';
import { Team } from '../../../landingPage/models/Team';
import { UserService } from '../../../landingPage/_services/userService';
import { TeamMember } from '../../../landingPage/models/TeamMember';
import { EditTeam } from '../../../landingPage/models/EditTeam';
import { EditTeamMember } from '../../../landingPage/models/EditTeamMember';
import { TeamService } from '../../../landingPage/_services/teamService';

@Component({
  selector: 'app-edit-team-dialog',
  templateUrl: './edit-team-dialog.component.html',
  styleUrls: ['./edit-team-dialog.component.css']
})
export class EditTeamDialogComponent implements OnInit {

  userList: SelectableUser[];
  selectedUsers: SelectableUser[];
  teamLeader: SelectableUser;
  editTeam: Team;


  constructor(
    private userService: UserService,
    private teamService: TeamService,
    public dialogRef: MatDialogRef<EditTeamDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.editTeam = data.team;
    this.userList = Array<SelectableUser>();
    this.selectedUsers = Array<SelectableUser>();
  }

  ngOnInit() {
    this.userService.GetUnassignedUsers()
      .subscribe((list: User[]) => {
        console.log(this.editTeam);
        this.editTeam.teamMembers.forEach((element: TeamMember) => {
          let selectUser = new SelectableUser(element.id, element.name, true)
          this.userList.push(selectUser);
          this.selectedUsers.push(selectUser);
          if(this.editTeam.teamLeaderId == element.id)
          {
            this.teamLeader = selectUser;
            console.log(this.teamLeader);
          }

        });

        list.forEach((element : User) => {
          this.userList.push(new SelectableUser(element.userId, element.lastName + ", " + element.firstName, false));
        });

      });
  }

  onSelect(user: SelectableUser) {
    var index = this.selectedUsers.indexOf(user, 0);
    if (index > -1) {
      this.selectedUsers.splice(index, 1);
    }
    else {
      this.selectedUsers.push(user);
    }
  }

  onSubmit(){
    let team = new EditTeam(
      this.editTeam.id,
      this.editTeam.name,
      this.editTeam.description,
      this.teamLeader.id,
      this.editTeam.teamManagerId,
      new Array<EditTeamMember>()
    );

    this.selectedUsers.forEach((user : SelectableUser) => {
      team.teamMembers.push(new EditTeamMember(user.id));
    });

    console.log(JSON.stringify(team));

    this.teamService.EditTeam(team)
      .subscribe(result => {
        if(result)
        {
          this.dialogRef.close();
        }
      });
  }

}

export class SelectableUser {
  constructor(public id: string,
    public name: string,
    public isSelected: boolean) { }
}
