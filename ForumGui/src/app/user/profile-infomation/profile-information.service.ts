import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';
import {Information} from '../model/information';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {User} from '../model1/user';
const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ProfileInformationService {
  private config = 'https://localhost:44375/api';
  private apiUser ="/users";
  constructor(
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
    private handleErrorService: HandleErrorService,
  ) { }


  /* GET: get information of user from server */
  getInformationOfCurrentUser(id: string):Observable<User>{
    const url = `${this.config}${this.apiUser}/${id}`;
    this.setTokenToHeader();
    return this.http.get<User>(url,httpOptions)
    .pipe(
      //catchError(this.handleErrorService.log('getInformationOfCurrentUser'))
    );
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

  /*PUT: update the Information on the server. Return the updated object upon success */
  updateInformation(user : User): Observable<User>{
    if(user.id != null){
      const url = `${this.config}${this.apiUser}/${user.id}`;
      this.setTokenToHeader();
      return this.http.put<User>(url,user,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateInformation',user))
      );
    }  
  }


  


}
