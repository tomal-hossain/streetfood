import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html'
})
export class SignUpComponent implements OnInit {

    signUpForm: FormGroup;
    isLoadingFlag: boolean;

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.signUpForm = this.fb.group({
            name: [null, [ Validators.required, Validators.minLength(6), Validators.maxLength(30)] ],
            email: [null, [ Validators.required, Validators.email ] ],
            password: [null, [ Validators.required, Validators.minLength(8), Validators.maxLength(30) ] ],
            confirmPassword: [null, [ Validators.required, this.matchPassword ] ],
        });
    }

    matchPassword = (control: FormControl): { [s: string]: boolean } => {
        if (!control.value) {
            return { required: true };
        } else if (control.value !== this.signUpForm.controls.password.value) {
            return { confirm: true, error: true };
        }
    }

    makeFormDirty(form: FormGroup) {
        Object.keys(form.controls).forEach(key => {
            const control = form.controls[key];
            control.markAsDirty();
            control.updateValueAndValidity();
        });
    }

    submitForm(): void {
        this.makeFormDirty(this.signUpForm);
        if  (this.signUpForm.valid) {
            this.isLoadingFlag = true;
        }
    }
}
