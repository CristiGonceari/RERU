import { AbstractControl, FormGroup, Validators } from '@angular/forms';

export class ValidatorUtil {
    public static isInvalidPattern(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).errors && form.get(field).errors.pattern;
    }

    public static isTouched(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).touched && form.get(field).dirty; 
    }

    public static isRequired(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).errors && form.get(field).errors.required;
    }

    public static isNotNullString(control: AbstractControl):{[key: string]: boolean} | null {
        if (control && control.value === 'null') {
            return { nullValidator: true };
        }

        return null;
    }

    public static isIdnpLengthValidator(form: FormGroup, field: string): boolean {
        return form.get(field) && form.get(field).errors && (form.get(field).errors.minlength || form.get(field).errors.maxlength)
    }

    public static handleStudyChange(event, form) {
        if (event.target.checked) {
          form.get('diplomaNumber').clearValidators();
          form.get('diplomaReleaseDay').clearValidators();
        } else {
          form.get('diplomaNumber').setValidators([Validators.required]);
          form.get('diplomaReleaseDay').setValidators([Validators.required]);
        }

        form.get('diplomaNumber').updateValueAndValidity();
        form.get('diplomaReleaseDay').updateValueAndValidity();
    }
}
