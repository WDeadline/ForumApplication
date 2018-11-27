import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';
import {Information} from '../model/information';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {CurrentUserInfo} from '../../authentication/model/current-user-info';
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
  private apiInformation ="/informations";
  constructor(
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
    private handleErrorService: HandleErrorService,
  ) { }


  /* GET: get Educations of all users from server */
  getInformationOfCurrentUser(id: string):Observable<Information>{
    const url = `${this.config}/users/${id}/information`;
    this.setTokenToHeader();
    return this.http.get<Information>(url,httpOptions)
    .pipe(
      //catchError(this.handleErrorService.log("dsds"))
      
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
  updateInformation(information : Information): Observable<Information>{
    if(information.id != null){
      const url = `${this.config}${this.apiInformation}/${information.id}`;
      this.setTokenToHeader();
      return this.http.put<Information>(url,information,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateInformation',information))
      );
    }else{
      const url = `${this.config}${this.apiInformation}`;
      this.setTokenToHeader();
      return this.http.post<Information>(url,information,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateInformation',information))
      );
    }
    
  }


  


}
