import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';
import {CurrentUserInfo} from '../model/current-user-info';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    //private loggedIn1: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.loggedIn());
    private apiLogin = '/login';
    private apiRegister = '/register';
    private config = 'https://localhost:44375/api';
    private loggedIn = new BehaviorSubject<boolean>(this.hasToken());
    currentUserInfo : CurrentUserInfo;
    constructor(
        private http: HttpClient, 
        private cookie : CookieService,
        private router : Router,  
        ) { }

    

    login(Username: string, pass: string) {
        return this.http.post<any>(`${this.config}${this.apiLogin}`, { password : pass, usernameOrEmailAddress : Username })
            .pipe(map(user => {
                    // login successful if there's a jwt token in the response
                    if (user && user.token) {
                        // store user details and jwt token in local storage to keep user logged in between page refreshes
                        localStorage.setItem('currentUser', JSON.stringify(user));
                        this.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
                        localStorage.setItem('userRole', this.currentUserInfo.roles[0]);
                        this.loggedIn.next(true);   
                        console.log("id: "+ this.currentUserInfo.id);                  
                    }
                return user;
            }));
    }

    register(FirstName: string, LastName: string, Username:string, EmailAddress: string, Password: string){
        return this.http.post<any>(`${this.config}${this.apiRegister}`,{FirstName,LastName,Username,EmailAddress,Password});
    }

    logOut() {
        this.loggedIn.next(false);
        localStorage.removeItem('currentUser');
        localStorage.removeItem('userRole');
        localStorage.clear();
        sessionStorage.clear();
        if(!this.hasToken()){
            //this.getUserInfo(); 
            console.log("logOut session false");
          }else{
            console.log("logOut session true");
          }
        this.router.navigate(['/login']);
    }

    isLoggedIn() {
        return this.loggedIn.asObservable(); 
    }

    getCurrentUser(){
        return this.currentUserInfo;
    }

    hasToken(){
        return (!!localStorage.getItem('currentUser'));
    }

    getUserRoleFromLocalStorage(): string {
        return localStorage.getItem('userRole');
    }



    getErrorLogin(err : any) {
        if(err.error){
            if(err.error.UsernameOrEmailAddress){
                return err.error.UsernameOrEmailAddress[0];
            } 
            //them lỗi dô          
        }
        return;
    }

    getErrorRegistor(err : any){
        if(err.error){
            if(err.error.Username){
                return err.error.Username[0];
            }
            if(err.error.EmailAddress){
                return err.error.EmailAddress[0];
            }
        }
        if(err.message) {
            return err.message;
        }
        return;
    }
}