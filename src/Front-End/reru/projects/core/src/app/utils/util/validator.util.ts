import { AbstractControl, FormGroup, Validators } from '@angular/forms';

export class ValidatorUtil{

    public static isIdnpLengthValidator(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).errors && (form.get(field).errors.minlength || form.get(field).errors.maxlength)
    }

    public static isInvalidPattern(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).errors && form.get(field).errors.pattern;
    }

    public static isTouched(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).touched && form.get(field).dirty; 
    }

    public static isNotNullString(control: AbstractControl):{[key: string]: boolean} | null {
        if (control && control.value === 'null') {
            return { nullValidator: true };
        }

        return null;
    }
}