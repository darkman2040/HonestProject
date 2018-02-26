import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ProjectTemplateTopLevel } from '../../../landingPage/models/ProjectTemplateTopLevel';
import { ProjectService } from '../../../landingPage/_services/projectService';

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
  
  constructor(private _formBuilder: FormBuilder,
  private projectService: ProjectService) { }

  ngOnInit() {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', [Validators.required, Validators.maxLength(50)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', [Validators.required, Validators.maxLength(200)]]
    });
    this.projectService.GetProjectTemplateTopLevel()
    .subscribe((templates: ProjectTemplateTopLevel[]) => {
      this.projectTemplates = templates;
    })
  }

  onSelectTemplate(template: ProjectTemplateTopLevel){
    console.log(JSON.stringify(template));
  }

}
