// import { Injectable } from '@angular/core';
// import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { AuthService } from '../services';

// @Injectable()
// export class JwtInterceptor implements HttpInterceptor {
//     constructor(private authenticationService: AuthService) { }

//     intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//         let currentUser: any; 
//         currentUser = this.authenticationService.currentUserValue;
//         if (currentUser && currentUser.Token) {
//             request = request.clone({
//                 setHeaders: {
//                     Authorization: `Bearer ${currentUser.Token}`
//                 }
//             });
//         }

//         return next.handle(request);
//     }
// }