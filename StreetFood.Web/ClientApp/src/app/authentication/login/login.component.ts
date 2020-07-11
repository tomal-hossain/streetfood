import { AuthService } from './../../../shared/service/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FormService } from 'src/shared/service/form.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

    loginForm: FormGroup;
    isLoadingFlag: boolean;
    errorMessage: string;
    redirectUrl: string;

    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private router: Router,
        private formService: FormService,
        private activatedRoute: ActivatedRoute) {
            if  (this.authService.isLoggedin()) {
                this.router.navigate(['food/all-foods']);
            }
        }

    ngOnInit() {
        this.activatedRoute.queryParams.subscribe(params => {
            this.redirectUrl = params.redirectURL;
        });
        this.loginForm = this.fb.group({
            email: [null, [ Validators.required, Validators.email ] ],
            password: [null, [ Validators.required ] ],
        });
    }

    submitForm(): void {
        this.errorMessage = null;
        this.formService.makeFormDirty(this.loginForm);
        if  (this.loginForm.valid) {
            this.isLoadingFlag = true;
            this.authService.signIn(this.loginForm.value)
                .subscribe(response => {
                    this.authService.saveToken(response);
                    if (this.redirectUrl) {
                        this.router.navigateByUrl(this.redirectUrl);
                    } else {
                        this.router.navigate(['food/all-foods']);
                    }
                }, error => {
                    this.isLoadingFlag = false;
                    if (error.status === 400) {
                        this.errorMessage = error.error;
                    }
                });
        }
    }
}
