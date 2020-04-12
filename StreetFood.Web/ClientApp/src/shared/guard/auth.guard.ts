import { AuthService } from './../service/auth.service';
import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

@Injectable()

export class AuthGuard {

    constructor(private authService: AuthService, private router: Router) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (!this.authService.isLoggedin()) {
            this.router.navigate(['authentication/login'], { queryParams: { redirectURL: state.url} });
            return false;
        }
        return true;
    }
}
