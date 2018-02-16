import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../landingPage/_services/userService';
import { User } from '../../../landingPage/models/User';
import { Team } from '../../../landingPage/models/Team';
import { TeamMember } from '../../../landingPage/models/TeamMember';
import { RegisterTeam } from '../../../landingPage/models/RegisterTeam';
import { RegisterTeamMember } from '../../../landingPage/models/RegisterTeamMember';
import { TeamService } from '../../../landingPage/_services/teamService';

@Component({
  selector: 'app-add-new-team-dialog',
  templateUrl: './add-new-team-dialog.component.html',
  styleUrls: ['./add-new-team-dialog.component.css']
})
export class AddNewTeamDialogComponent implements OnInit {

  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  userList: User[];
  selectedUsers: User[];
  teamLeader: User;

  constructor(private _formBuilder: FormBuilder,
    private userService: UserService,
    private teamService: TeamService,
    public dialogRef: MatDialogRef<AddNewTeamDialogComponent>) { }

  ngOnInit() {
    this.selectedUsers = new Array<User>();
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', [Validators.required, Validators.maxLength(50)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', [Validators.required, Validators.maxLength(200)]]
    });

    this.userService.GetUnassignedUsers()
      .subscribe((list: User[]) => {
        console.log(list);
        this.userList = list;
      });

  }

  onSelect(user: User) {
    var index = this.selectedUsers.indexOf(user, 0);
    if (index > -1) {
      this.selectedUsers.splice(index, 1);
    }
    else {
      this.selectedUsers.push(user);
    }
  }

  onSubmit() {
    const formModel = this.firstFormGroup.value;
    const formModel2 = this.secondFormGroup.value;
    let teamMember: RegisterTeamMember[] = new Array<RegisterTeamMember>();
    this.selectedUsers.forEach(user => {
      let member: RegisterTeamMember = new RegisterTeamMember(user.userId);
      console.log(user)
      teamMember.push(member);
    });
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    let userId = currentUser && currentUser.userId;
    let team = new RegisterTeam(formModel.firstCtrl,
      formModel2.secondCtrl,
      this.teamLeader.userId,
      userId,
      teamMember);
      this.teamService.RegisterNewTeam(team)
      .subscribe(result => {
        if(result)
        {
          this.dialogRef.close();
        }
      });
  } 

}

