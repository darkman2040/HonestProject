import { WorkTypeHours } from "../models/work-type-hours";
import { FormControl, Validators, FormGroup } from "@angular/forms";
import { Injectable } from "@angular/core";
import { ProjectTemplateWorkType } from "../../../../landingPage/models/ProjectTemplateWorkType";
import { User } from "../../../../landingPage/models/User";
import { FormTaskUserPercentage } from "../models/form-task-user-percentage";
import { SliderControl } from "../models/slider-control";

@Injectable()
export class ProjectWorkTypeControlService {
    constructor() { }

    workTypeHoursToFormGroup(workTypes: WorkTypeHours[]) {
        let group: any = {};

        workTypes.forEach(workType => {
            group[workType.key] = workType.required ? new FormControl(workType.value || '', Validators.required)
                : new FormControl(workType.value || '');
        });

        return new FormGroup(group);

    }

    taskTypeAndUserToFormGroups(workTypes: ProjectTemplateWorkType[], users: User[]) : FormTaskUserPercentage[] {
        let groupList = new Array<FormTaskUserPercentage>();
        workTypes.forEach((workType: ProjectTemplateWorkType) => {
            let group: any = {};
            let controls = new Array<SliderControl>();
            users.forEach((user: User) => {
                let control = new FormControl('');
                controls.push(new SliderControl(user, control));
                group[user.userId] = control;
            });

            groupList.push(new FormTaskUserPercentage(workType.name, new FormGroup(group), controls));
        });

        return groupList;
    }
}