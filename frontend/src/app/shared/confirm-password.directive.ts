import { AbstractControl } from "@angular/forms";
import { ValidatorFn } from "@angular/forms";

export function confirmPasswordValidator(): ValidatorFn {
    return (control: AbstractControl): {[key: string]: any} | null => {
        const isSame = control.value === (control.root.get("password") === null ? "" : control.root.get("password").value);
        return isSame ? null : {'confirmPassword': {value: control.value}};
    };
}