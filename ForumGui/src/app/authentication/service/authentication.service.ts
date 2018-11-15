import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import {CurrentUserInfo} from '../model/current-user-info';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    constructor(
        private http: HttpClient, 
        private cookie : CookieService,
        private router : Router,  
        ) { }

    //private loggedIn1: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.loggedIn());
    private loggedIn = new BehaviorSubject<boolean>(this.hadToken());
    login(Username: string, pass: string) {
        return this.http.post<any>(`https://localhost:44375/api/login`, { password : pass, usernameOrEmailAddress : Username })
            .pipe(map(user => {
                    // login successful if there's a jwt token in the response
                    if (user && user.token) {
                        // store user details and jwt token in local storage to keep user logged in between page refreshes
                        localStorage.setItem('currentUser', JSON.stringify(user));
                        this.loggedIn.next(true);
                        let currentUserInfo : CurrentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
                        console.log("currentUserInfo"+currentUserInfo.roles[0]);
                    }
                return user;
            }));
    }

    register(FirstName: string, LastName: string, Username:string, EmailAddress: string, Password: string){
        return this.http.post<any>(`https://localhost:44375/api/register`,{FirstName,LastName,Username,EmailAddress,Password});
    }

    logOut() {
        this.loggedIn.next(false);
        localStorage.removeItem('currentUser');
        this.router.navigate(['/login']);
    }

    isLoggedIn() {
        return this.loggedIn.asObservable(); 
    }

    hadToken(){
        return (!!localStorage.getItem('currentUser'));
    }

    getUserRoleFromLocalStorage(): string {
        return localStorage.getItem('userRole');
    }

    getErrorLogin(err : any) {
        if(err.error){
            if(err.error.UsernameOrEmailAddress[0]){
                return err.error.UsernameOrEmailAddress[0];
            }           
        }
        return;
    }
}