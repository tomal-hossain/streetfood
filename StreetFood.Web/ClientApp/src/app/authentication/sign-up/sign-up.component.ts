import { FormService } from './../../../shared/service/form.service';
import { AuthService } from './../../../shared/service/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ISignUp } from 'src/shared/interface/sign-up';
import { Router } from '@angular/router';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html'
})
export class SignUpComponent implements OnInit {

    signUpForm: FormGroup;
    isLoadingFlag: boolean;
    errorMessage: string;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
        private formService: FormService
        ) { }

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

    submitForm(formValue): void {
        this.errorMessage = null;
        this.formService.makeFormDirty(this.signUpForm);
        if  (this.signUpForm.valid) {
            this.isLoadingFlag = true;
            const model: ISignUp = {
                name: formValue.name,
                email: formValue.email,
                password: formValue.password
            };
            this.authService.signUp(model)
                .subscribe(response => {
                    this.router.navigate(['authentication/registration-success']);
                }, error => {
                    this.isLoadingFlag = false;
                    if (error.status === 400) {
                        this.errorMessage = error.error;
                    }
                });
        }
    }
}
