import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ISignIn } from '../interface/sign-in';
import { ISignUp } from '../interface/sign-up';
import { IEmail } from '../interface/email';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    apiUrl: string;

    constructor(private http: HttpClient) {
        this.apiUrl = environment.apiUrl;
    }

    signIn(model: ISignIn) {
        return this.http.post(this.apiUrl + '/login', model);
    }

    signUp(model: ISignUp) {
        return this.http.post(this.apiUrl + '/signup', model);
    }

    forgotPassword(model: IEmail) {
        return this.http.post(this.apiUrl + '/resetpassword', model);
    }

    saveToken(token: string) {
        localStorage.setItem('userToken', token);
    }

    getToken(): string {
        return localStorage.getItem('userToken');
    }

    isLoggedin(): boolean {
        if  (localStorage.getItem('userToken')) {
            return true;
        }
        return false;
    }

    logOut() {
        localStorage.clear();
    }
}
