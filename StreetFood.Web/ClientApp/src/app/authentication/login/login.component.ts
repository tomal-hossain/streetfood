import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

    loginForm: FormGroup;
    isLoadingFlag: boolean;

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.loginForm = this.fb.group({
            email: [null, [ Validators.required, Validators.email ] ],
            password: [null, [ Validators.required ] ],
        });
    }

    makeFormDirty(form: FormGroup) {
        Object.keys(form.controls).forEach(key => {
            const control = form.controls[key];
            control.markAsDirty();
            control.updateValueAndValidity();
        });
    }

    submitForm(): void {
        this.makeFormDirty(this.loginForm);
        if  (this.loginForm.valid) {
            this.isLoadingFlag = true;
        }
    }
}
