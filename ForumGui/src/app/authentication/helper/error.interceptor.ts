import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from '../service/authentication.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) {}
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {    
        return next.handle(request).pipe(catchError(err => {
            /*if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.authenticationService.logOut();
                location.reload(true);
            }*/
            /*if (err.status === 400) {
                if(err.error){
                    if(err.error.UsernameOrEmailAddress){
                        console.log(err.error.UsernameOrEmailAddress[0]);
                    }
                    if(err.error.Password){
                        console.log(err.error.Password[0]);
                    }
                }
            }*/
            console.log(err);
            
            return throwError(err);
        }))
    }
}