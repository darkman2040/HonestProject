import { WorkTypeHours } from "../models/work-type-hours";
import { FormControl, Validators, FormGroup } from "@angular/forms";
import { Injectable } from "@angular/core";
import { UserTaskPercent } from "../models/user-task-percent";

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

    userTaskPctToFormGroup(userTaskPcts: UserTaskPercent[]){
        let group: any = {};
        userTaskPcts.forEach(workType => {
            group[workType.user.userId] = new FormControl('');
        });

        return new FormGroup(group);
    }
}