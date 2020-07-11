import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../service/auth.service';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable()

export class TokenInterceptor implements HttpInterceptor {

    constructor(public authervice: AuthService, private router: Router) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${this.authervice.getToken()}`
            }
        });
        return next.handle(request).pipe( tap(() => {},
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    if (err.status !== 401 && err.status !== 403) {
                        return;
                    }
                    this.router.navigate(['authentication/login']);
                }
        }));
    }
}
