import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import {User} from '../../user/model1/user';
const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class AdminUserService {

  private apiUser = '/users'; 
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

      /* GET: get Experience of user from server */
      getAllUser(): Observable<User[]>{
        const url = `${this.config}${this.apiUser}`;
        this.setTokenToHeader();
        return this.http.get<User[]>(url,httpOptions )
          .pipe(
            catchError(this.handleErrorService.handleError('getAllUser',[]))
          )
      }
      setTokenToHeader(){
        let tokenString :string;
        if(this.anthenticationService.hasToken()){
          tokenString = this.anthenticationService.getToken();
          httpOptions.headers =
          httpOptions.headers.set('Authorization', `Bearer ${tokenString}`);
          console.log("this.tokenString: " + tokenString);
        }
      }
}
