import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-forgot-password',
    templateUrl: './forgot-password.component.html'
})
export class ForgotPasswordComponent implements OnInit {

    forgotPasswordForm: FormGroup;
    isLoadingFlag: boolean;

    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.forgotPasswordForm = this.fb.group({
            email: [null, [Validators.required, Validators.email] ]
        });
    }


    submitForm() {
        if  (this.forgotPasswordForm.valid) {
            this.isLoadingFlag = true;
        }
    }

}
