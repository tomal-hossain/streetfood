import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { NzCardModule } from 'ng-zorro-antd/card';
import { ReactiveFormsModule } from '@angular/forms';
import { NzFormModule, NzInputModule, NzButtonModule, NzGridModule, NzSpinModule } from 'ng-zorro-antd';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations: [
        LoginComponent,
        SignUpComponent,
        ForgotPasswordComponent
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
        NzSpinModule
    ],
    providers: [],
})
export class AutheticationModule { }
