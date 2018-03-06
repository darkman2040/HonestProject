import { FormGroup, FormControl } from "@angular/forms";
import { SliderControl } from "./slider-control";

export class FormTaskUserPercentage {
    constructor(public workName: string,
        public group: FormGroup,
        public sliderControls: SliderControl[]) { }
}