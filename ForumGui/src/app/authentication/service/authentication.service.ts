import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    constructor(private http: HttpClient) { }

    login(Username: string, pass: string) {
        return this.http.post<any>(`https://localhost:44375/api/login`, { password : pass, usernameOrEmailAddress : Username })
            .pipe(map(user => {
                    // login successful if there's a jwt token in the response
                    if (user && user.token) {
                        // store user details and jwt token in local storage to keep user logged in between page refreshes
                        localStorage.setItem('currentUser', JSON.stringify(user));
                        console.log("user.token"+user.token);
                        console.log("user"+user);
                        //onsole.log("localStorage"+localStorage.getItem('currentUser').)
                    }

                return user;
            }));
    }

    register(FirstName: string, LastName: string, Username:string, EmailAddress: string, Password: string){
        return this.http.post<any>(`https://localhost:44375/api/register`,{FirstName,LastName,Username,EmailAddress,Password});
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
    /*
    getToken() {
        return localStorage.getItem('currentUser');
    }

    getUserRoleFromLocalStorage(): string {
        return localStorage.getItem('currentUser');
      }
    */

   handleError(err) {
    if(err.error){
      if(err.error.errors){
          console.log("err.error.errors[0].defaultMessage"+err.error.errors[0].defaultMessage);
        return err.error.errors[0].defaultMessage;
      }
      console.log("err.error.message"+err.error.message);
      return err.error.message;
    }
    return;
  }
}