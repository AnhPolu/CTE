import { inject, Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenService } from '../services/authen.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    authenticationService: AuthenService = inject(AuthenService);
    constructor() { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                //this.authenticationService.logOut();
                location.reload();
            }

            const error = err.error.message || err.statusText;
            return throwError(error);
        }))
    }
}