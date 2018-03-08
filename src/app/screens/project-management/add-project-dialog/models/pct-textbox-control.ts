import { User } from "../../../../landingPage/models/User";
import { FormControl } from "@angular/forms";

export class PctTextBoxControl{
    constructor(public user: User,
        public control: FormControl
    ) {}
}