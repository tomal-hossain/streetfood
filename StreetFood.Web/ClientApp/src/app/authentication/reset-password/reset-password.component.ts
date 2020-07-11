import { FormService } from './../../../shared/service/form.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { AuthService } from 'src/shared/service/auth.service';
import { ISignIn } from 'src/shared/interface/sign-in';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-reset-password',
    templateUrl: './reset-password.component.html'
})
export class ResetPasswordComponent implements OnInit {

    resetPasswordForm: FormGroup;
    isLoadingFlag: boolean;
    isErrorMesage: boolean;
    token: string;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private formService: FormService
        ) { }

    ngOnInit() {
        this.activatedRoute.paramMap.subscribe(params => {
            this.token = params.get('token');
        });
        this.resetPasswordForm = this.fb.group({
            password: [null, [Validators.required, Validators.minLength(8)] ],
            confirmPassword: [null, [Validators.required, this.matchPassword] ]
        });
    }

    matchPassword = (control: FormControl): { [s: string]: boolean } => {
        if (!control.value) {
            return { required: true };
        } else if (control.value !== this.resetPasswordForm.controls.password.value) {
            return { confirm: true, error: true };
        }
    }

    submitForm() {
        this.isErrorMesage = false;
        this.formService.makeFormDirty(this.resetPasswordForm);
        if  (this.resetPasswordForm.valid) {
            this.isLoadingFlag = true;
            const model: ISignIn = {
                email: null,
                password: this.resetPasswordForm.value.password
            };
            this.authService.resetPassword(model, this.token)
                .subscribe(response => {
                    this.router.navigate(['authentication/confirm-success']);
                }, error => {
                    this.isErrorMesage = true;
                    this.isLoadingFlag = false;
                });
        }
    }
}
