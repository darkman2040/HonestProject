import { WorkTypeHours } from "../models/work-type-hours";
import { FormControl, Validators, FormGroup } from "@angular/forms";
import { Injectable } from "@angular/core";

@Injectable()
export class ProjectWorkTypeControlService {
    constructor() { }

    toFormGroup(workTypes: WorkTypeHours[]) {
        let group: any = {};

        workTypes.forEach(workType => {
            group[workType.key] = workType.required ? new FormControl(workType.value || '', Validators.required)
                : new FormControl(workType.value || '');
        });

        console.log(JSON.stringify(group));

        return new FormGroup(group);

    }
}