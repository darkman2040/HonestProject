import { User } from "../../../../landingPage/models/User";
import { FormControl } from "@angular/forms";

export class SliderControl{
    constructor(public user: User,
        public control: FormControl
    ) {}
}