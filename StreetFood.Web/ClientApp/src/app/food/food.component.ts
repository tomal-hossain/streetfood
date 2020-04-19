import { AuthService } from './../../shared/service/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-food',
    templateUrl: './food.component.html'
})
export class FoodComponent implements OnInit {

    isLogin: boolean;

    constructor(private authService: AuthService) { }

    ngOnInit() {
        this.isLogin = this.authService.isLoggedin();
    }

    logout() {
        this.authService.logOut();
        this.isLogin = false;
    }

}
