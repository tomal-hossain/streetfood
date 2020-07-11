import { FormService } from './../../../shared/service/form.service';
import { AuthService } from './../../../shared/service/auth.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-forgot-password',
    templateUrl: './forgot-password.component.html'
})
export class ForgotPasswordComponent implements OnInit {

    forgotPasswordForm: FormGroup;
    isLoadingFlag: boolean;
    errorMessage: string;
    isSuccessMessage = false;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private formService: FormService,
        ) { }

    ngOnInit() {
        this.forgotPasswordForm = this.fb.group({
            email: [null, [Validators.required, Validators.email] ]
        });
    }

    submitForm() {
        this.errorMessage = null;
        this.isSuccessMessage = false;
        this.formService.makeFormDirty(this.forgotPasswordForm);
        if  (this.forgotPasswordForm.valid) {
            this.isLoadingFlag = true;
            this.authService.forgotPassword(this.forgotPasswordForm.value)
                .subscribe(response => {
                    this.isSuccessMessage = true;
                    this.isLoadingFlag = false;
                }, error => {
                    if (error.status === 400) {
                        this.errorMessage = error.error;
                    } else {
                        this.errorMessage = 'Something went wrong. Please try again.';
                    }
                    this.isLoadingFlag = false;
                });
        }
    }

}
