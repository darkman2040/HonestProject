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
import { FormTaskUserPercentage } from './models/form-task-user-percentage';
import { SliderControl } from './models/slider-control';

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
  projectTemplateWorkTypeFormGroups: FormTaskUserPercentage[];
  selectedTimeFrame: string;
  projectColor: string;
  colors: any;


  constructor(private _formBuilder: FormBuilder,
    private projectService: ProjectService,
    private userService: UserService,
    private templateControlService: ProjectWorkTypeControlService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
      this.colors = [
        {color: 'Green'},
        {color: 'Blue'},
        {color: 'Orange'},
        {color: 'Red'},
        {color: 'Yellow'},
        {color: 'Purple'},
        {color: 'Pink'},
        {color: 'Brown'},
        {color: 'Black'},
        {color: 'Aqua'},
        {color: 'royalblue'},
        {color: 'teal'},
        {color: 'yeallowgreen'}
      ]

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

    this.selectedTimeFrame = "days";
    this.projectColor = "black";

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

  onTaskSetupComplete() {
    this.projectTemplateWorkTypes.forEach((projectTemplateWorkType: ProjectTemplateWorkType) => {
      console.log(JSON.stringify(this.workTypeFormGroup.value[projectTemplateWorkType.name])); //KEEP THIS UNTIL READY TO PULL DATA FOR REGISTRATION
      this.projectTemplateWorkTypeFormGroups = this.templateControlService.taskTypeAndUserToFormGroups(this.projectTemplateWorkTypes, this.users)

    });

  }

  onSliderChange(formGroup: FormGroup, slider: SliderControl) {
    console.log(JSON.stringify(formGroup.value[slider.user.userId]));
    this.computeUserPercent(slider.user);
  }

  onColorClick(color: string){
    this.projectColor = color;
  }

  computeUserPercent(user: User) {
    let pctNumber: number = 0;
    this.projectTemplateWorkTypeFormGroups.forEach((form: FormTaskUserPercentage) => {
      pctNumber = pctNumber + form.group.value[user.userId];
    });

    this.pctUsers.forEach((pct: UserPercent) => {
      if(pct.user == user)
      {
        pct.pct = pctNumber;
      }
    });
    
  }
}

export class UserPercent {
  constructor(public user: User,
    public pct: number) { }
}

export class ColorList {

}


