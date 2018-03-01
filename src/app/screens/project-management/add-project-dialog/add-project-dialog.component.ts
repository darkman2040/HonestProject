import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProjectTemplateTopLevel } from '../../../landingPage/models/ProjectTemplateTopLevel';
import { ProjectService } from '../../../landingPage/_services/projectService';
import { User } from '../../../landingPage/models/User';
import { UserService } from '../../../landingPage/_services/userService';
import { MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-add-project-dialog',
  templateUrl: './add-project-dialog.component.html',
  styleUrls: ['./add-project-dialog.component.css']
})
export class AddProjectDialogComponent implements OnInit {

  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  projectTemplates: ProjectTemplateTopLevel[];
  selectedTemplate: ProjectTemplateTopLevel;
  pctUsers: UserPercent[];


  constructor(private _formBuilder: FormBuilder,
    private projectService: ProjectService,
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', [Validators.required, Validators.maxLength(50)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', [Validators.required, Validators.maxLength(200)]]
    });
    console.log(JSON.stringify(this.data.teamId));
    this.projectService.GetProjectTemplateTopLevel()
      .subscribe((templates: ProjectTemplateTopLevel[]) => {
        this.projectTemplates = templates;
      });
      this.userService.GetTeamMembers(this.data.teamId)
      .subscribe((users: User[]) => {
        this.pctUsers = new Array<UserPercent>();
        users.forEach((user: User) =>{
          this.pctUsers.push(new UserPercent(user, 0));
        });
      });
  }

  onSelectTemplate(template: ProjectTemplateTopLevel) {

  }

}

export class UserPercent {
  constructor(public user: User,
    public pct: number) { }
}
