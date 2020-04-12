import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { NzCardModule } from 'ng-zorro-antd/card';
import { ReactiveFormsModule } from '@angular/forms';
import { NzFormModule, NzInputModule, NzButtonModule, NzGridModule, NzSpinModule, NzAlertModule } from 'ng-zorro-antd';
import { CommonModule } from '@angular/common';
import { RegistraionSuccessComponent } from './registraion-success/registraion-success.component';
import { ConfirmSuccessComponent } from './confirm-success/confirm-success.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

@NgModule({
    declarations: [
        LoginComponent,
        SignUpComponent,
        ForgotPasswordComponent,
        RegistraionSuccessComponent,
        ConfirmSuccessComponent,
        ResetPasswordComponent
    ],
    imports: [
        CommonModule,
        AuthenticationRoutingModule,
        NzCardModule,
        NzFormModule,
        NzInputModule,
        ReactiveFormsModule,
        NzButtonModule,
        NzGridModule,
        NzSpinModule,
        NzAlertModule
    ],
    providers: [],
})
export class AutheticationModule { }
