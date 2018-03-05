import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ProjectTemplateTopLevel } from '../../../landingPage/models/ProjectTemplateTopLevel';
import { ProjectService } from '../../../landingPage/_services/projectService';
import { User } from '../../../landingPage/models/User';
import { UserService } from '../../../landingPage/_services/userService';
import { MAT_DIALOG_DATA } from '@angular/material';
import { ProjectWorkTypeControlService } from './services/project-work-type-control.service';
import { ProjectTemplateWorkType } from '../../../landingPage/models/ProjectTemplateWorkType';
import { WorkTypeHours } from './models/work-type-hours';
import { UserTaskPercent } from './models/user-task-percent';

@Component({
  selector: 'app-add-project-dialog',
  templateUrl: './add-project-dialog.component.html',
  styleUrls: ['./add-project-dialog.component.css']
})
export class AddProjectDialogComponent implements OnInit {

  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  workTypeFormGroup: FormGroup;
  projectTemplates: ProjectTemplateTopLevel[];
  selectedTemplate: ProjectTemplateTopLevel;
  pctUsers: UserPercent[];
  workTypeControls: WorkTypeHours[];
  projectTemplateWorkTypes: ProjectTemplateWorkType[];
  users: User[];
  projectTemplateWorkTypeFormGroups: FormGroup[];


  constructor(private _formBuilder: FormBuilder,
    private projectService: ProjectService,
    private userService: UserService,
    private templateControlService: ProjectWorkTypeControlService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', [Validators.required, Validators.maxLength(50)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', [Validators.required, Validators.maxLength(200)]]
    });

    this.workTypeFormGroup = new FormGroup({
      fake: new FormControl()
    });

    this.projectTemplateWorkTypeFormGroups = new Array<FormGroup>();

    this.projectService.GetProjectTemplateTopLevel()
      .subscribe((templates: ProjectTemplateTopLevel[]) => {
        this.projectTemplates = templates;
      });
    this.userService.GetTeamMembers(this.data.teamId)
      .subscribe((users: User[]) => {
        this.users = users;
        this.pctUsers = new Array<UserPercent>();
        users.forEach((user: User) => {
          this.pctUsers.push(new UserPercent(user, 0));
        });
      });
  }

  onSelectTemplate(template: ProjectTemplateTopLevel) {
    this.selectedTemplate = template;
    this.projectService.GetProjectTemplateWorkTypes(this.selectedTemplate.id)
      .subscribe((projectTemplates: ProjectTemplateWorkType[]) => {
        this.projectTemplateWorkTypes = projectTemplates;
        this.workTypeControls = new Array<WorkTypeHours>();
        projectTemplates.forEach((projectTemplate: ProjectTemplateWorkType) => {
          let work = new WorkTypeHours({
            value: 0,
            key: projectTemplate.name,
            label: projectTemplate.name,
            required: true
          });
          this.workTypeControls.push(work);
        });
        this.workTypeFormGroup = this.templateControlService.workTypeHoursToFormGroup(this.workTypeControls);
      })
  }

  onTaskSetupComplete(){
    this.projectTemplateWorkTypeFormGroups = new Array<FormGroup>();
    this.projectTemplateWorkTypes.forEach((projectTemplateWorkType: ProjectTemplateWorkType) => {
      console.log(JSON.stringify(this.workTypeFormGroup.value[projectTemplateWorkType.name])); //KEEP THIS UNTIL READY TO PULL DATA FOR REGISTRATION

    })
    
  }
}

export class UserPercent {
  constructor(public user: User,
    public pct: number) { }
}


