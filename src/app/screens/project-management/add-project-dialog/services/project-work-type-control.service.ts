import { WorkTypeHours } from "../models/work-type-hours";
import { FormControl, Validators, FormGroup } from "@angular/forms";
import { Injectable } from "@angular/core";
import { ProjectTemplateWorkType } from "../../../../landingPage/models/ProjectTemplateWorkType";
import { User } from "../../../../landingPage/models/User";
import { FormTaskUserPercentage } from "../models/form-task-user-percentage";
import { SliderControl } from "../models/slider-control";
import { NumberValidator } from "../../../../landingPage/validators/NumberValidator";
import { PctTextBoxControl } from "../models/pct-textbox-control";

@Injectable()
export class ProjectWorkTypeControlService {
    constructor() { }

    workTypeHoursToFormGroup(workTypes: WorkTypeHours[]) {
        let group: any = {};

        workTypes.forEach(workType => {
            group[workType.key] = new FormControl(workType.value || '', [Validators.required, Validators.min(1), NumberValidator]);
        });

        return new FormGroup(group);

    }

    taskTypeAndUserToFormGroups(workTypes: ProjectTemplateWorkType[], users: User[]) : FormTaskUserPercentage[] {
        let groupList = new Array<FormTaskUserPercentage>();
        workTypes.forEach((workType: ProjectTemplateWorkType) => {
            let group: any = {};
            let sliderControls = new Array<SliderControl>();
            let textboxControls = new Array<PctTextBoxControl>();
            users.forEach((user: User) => {
                let control = new FormControl('');
                let textControl = new FormControl('0', [Validators.required, Validators.min(0), Validators.max(100), NumberValidator])
                sliderControls.push(new SliderControl(user, control));
                textboxControls.push(new PctTextBoxControl(user, textControl));
                group[user.userId] = control;
                group[user.userId + 'text'] = textControl;
            });

            groupList.push(new FormTaskUserPercentage(workType.name, new FormGroup(group), sliderControls, textboxControls));
        });

        return groupList;
    }
}