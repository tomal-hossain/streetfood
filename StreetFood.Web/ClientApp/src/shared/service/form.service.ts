import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
    providedIn: 'root'
})

export class FormService {

    constructor() {}

    makeFormDirty(form: FormGroup) {
        Object.keys(form.controls).forEach(key => {
            const control = form.controls[key];
            control.markAsDirty();
            control.updateValueAndValidity();
        });
    }
}
