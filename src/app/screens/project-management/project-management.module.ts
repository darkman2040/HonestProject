import { NgModule } from "@angular/core";
import { ProjectManagementComponent } from "./project-management.component";
import { AddProjectDialogComponent } from "./add-project-dialog/add-project-dialog.component";
import { ProjectWorkTypeControlService } from "./add-project-dialog/services/project-work-type-control.service";
import { MaterialModule } from "../../angularmaterials.module";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

@NgModule({
    imports: [
        CommonModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [

        ProjectManagementComponent,
        AddProjectDialogComponent
    ],
    providers: [
        ProjectWorkTypeControlService
    ],
    entryComponents: [AddProjectDialogComponent]
})

export class ProjectManagementModule { }