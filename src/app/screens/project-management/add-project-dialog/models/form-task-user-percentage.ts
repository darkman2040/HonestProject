import { FormGroup, FormControl } from "@angular/forms";
import { SliderControl } from "./slider-control";
import { PctTextBoxControl } from "./pct-textbox-control";

export class FormTaskUserPercentage {
    constructor(public workName: string,
        public group: FormGroup,
        public sliderControls: SliderControl[],
        public textControls: PctTextBoxControl[]) { }
}