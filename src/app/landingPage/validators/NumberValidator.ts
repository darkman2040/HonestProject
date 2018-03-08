import { Validator, FormControl, ValidationErrors } from "@angular/forms";

export function NumberValidator(c: FormControl): ValidationErrors {

    const number = Number(c.value);
    const isValid = !isNaN(number);
    const message = {
        number: {
            valid: false
        }
    }
    return isValid ? null : message
}