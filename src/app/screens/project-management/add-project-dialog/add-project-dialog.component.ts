import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ProjectTemplateTopLevel } from '../../../landingPage/models/ProjectTemplateTopLevel';
import { ProjectService } from '../../../landingPage/_services/projectService';
import { User } from '../../../landingPage/models/User';
import { UserService } from '../../../landingPage/_services/userService';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { ProjectWorkTypeControlService } from './services/project-work-type-control.service';
import { ProjectTemplateWorkType } from '../../../landingPage/models/ProjectTemplateWorkType';
import { WorkTypeHours } from './models/work-type-hours';
import { UserTaskPercent } from './models/user-task-percent';
import { FormTaskUserPercentage } from './models/form-task-user-percentage';
import { SliderControl } from './models/slider-control';
import { RegisterProjectWorkType } from '../../../landingPage/models/RegisterProjectWorkType';
import { RegisterProjectTimePct } from '../../../landingPage/models/RegisterProjectTimePct';
import { RegisterProject } from '../../../landingPage/models/RegisterProject';
import { PctTextBoxControl } from './models/pct-textbox-control';

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
    public dialogRef: MatDialogRef<AddProjectDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.colors = [
      { color: 'Red' },
      { color: 'Yellow' },
      { color: 'Orange' },
      { color: 'Green' },
      { color: 'Blue' },
      { color: 'Purple' },
      { color: 'Pink' },
      { color: 'Brown' },
      { color: 'Black' },
      { color: 'Aqua' },
      { color: 'royalblue' },
      { color: 'teal' },
      { color: 'yeallowgreen' }
    ]

  }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', [Validators.required, Validators.maxLength(50)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      descriptionCtrl: ['', [Validators.required, Validators.maxLength(200)]],
      projectStartCtrl: ['', [Validators.required]],
      percentTime: ['', [Validators.required]]
    });

    this.workTypeFormGroup = new FormGroup({
      fake: new FormControl()
    });

    this.selectedTimeFrame = "months";
    this.projectColor = "blue";

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
      this.projectTemplateWorkTypeFormGroups = this.templateControlService.taskTypeAndUserToFormGroups(this.projectTemplateWorkTypes, this.users)
    });

  }

  onSliderChange(formGroup: FormTaskUserPercentage, slider: SliderControl) {
    formGroup.group.value[slider.user.userId + 'text'] = +slider.control.value;
    formGroup.textControls.forEach((textBox: PctTextBoxControl) => {
      if(textBox.user.userId == slider.user.userId)
      {
        textBox.control.setValue(slider.control.value);
      }
    });
    this.computeUserPercent(slider.user);
  }

  onTextChange(formGroup: FormTaskUserPercentage, slider: SliderControl) {
    let textBoxControl : PctTextBoxControl;
    formGroup.textControls.forEach((textBox: PctTextBoxControl) => {
      if(textBox.user.userId == slider.user.userId)
      {
        textBoxControl = textBox;
      }
    });

    formGroup.group.value[slider.user.userId] = +textBoxControl.control.value;
    slider.control.setValue(textBoxControl.control.value);
    this.computeUserPercent(slider.user);
  }

  onColorClick(color: string) {
    this.projectColor = color;
  }

  computeUserPercent(user: User) {
    let pctNumber: number = 0;
    this.projectTemplateWorkTypeFormGroups.forEach((form: FormTaskUserPercentage) => {
      pctNumber = +pctNumber + (+form.group.value[user.userId]);
    });

    this.pctUsers.forEach((pct: UserPercent) => {
      if (pct.user == user) {
        pct.pct = pctNumber;
      }
    });

  }

  getManHours(timeUnits: number)
  {
    if(this.selectedTimeFrame == "months")
    {
      return timeUnits * 30 * 8; // Replace with site specific info for number of hours in a day
    }

    if(this.selectedTimeFrame == "weeks")
    {
      return timeUnits * 5 * 8; // Replace with site specific info for number of hours in a day and to include weekends
    }

    if(this.selectedTimeFrame == "days")
    {
      return timeUnits * 8; // Replace with site specific info for number of hours in a day
    }

    return timeUnits;
  }

  onSubmit() {
    const nameModel = this.firstFormGroup.value;
    const detailsForm = this.secondFormGroup.value;
    let newProject: RegisterProject = new RegisterProject(
      nameModel.firstCtrl,
      detailsForm.descriptionCtrl,
      detailsForm.projectStartCtrl,
      detailsForm.percentTime,
      this.projectColor,
      new Array<RegisterProjectWorkType>()
    );

    this.projectTemplateWorkTypes.forEach((projectTemplateWorkType: ProjectTemplateWorkType) => {
      let newProjectWorkType: RegisterProjectWorkType = new RegisterProjectWorkType(projectTemplateWorkType.name,
        this.getManHours(this.workTypeFormGroup.value[projectTemplateWorkType.name]),
        new Array<RegisterProjectTimePct>());
        let workForm : FormTaskUserPercentage = this.projectTemplateWorkTypeFormGroups.find(x => x.workName == projectTemplateWorkType.name);
        workForm.sliderControls.forEach((slider: SliderControl) => {
          if(slider.control.value > 0){
            let registerTime: RegisterProjectTimePct = new RegisterProjectTimePct(workForm.group.value[slider.user.userId], slider.user.userId);
            newProjectWorkType.timePctWorkItems.push(registerTime);
          }
        })
        
      newProject.workTypeItems.push(newProjectWorkType);
    });

    this.projectService.RegisterNewProject(newProject)
    .subscribe(result => {
      if(result)
      {
        this.dialogRef.close();
      }
    })

  };


}

export class UserPercent {
  constructor(public user: User,
    public pct: number) { }
}

export class ColorList {

}


