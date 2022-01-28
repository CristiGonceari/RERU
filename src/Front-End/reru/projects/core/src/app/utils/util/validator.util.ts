import { AbstractControl, FormGroup, Validators } from '@angular/forms';

export class ValidatorUtil{

    public static isIdnpLengthValidator(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).errors && (form.get(field).errors.minlength || form.get(field).errors.maxlength)
    }
}