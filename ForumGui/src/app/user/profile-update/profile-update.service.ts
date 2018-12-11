import { Injectable,EventEmitter,Output } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Avatar} from '../model1/avatar';
import { catchError, map, tap } from 'rxjs/operators';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import {CurrentUserInfo} from '../../authentication/model/current-user-info';

const httpOptions = {
  headers: new HttpHeaders({ 
  })
};
@Injectable({
  providedIn: 'root'
})

export class ProfileUpdateService {

  private apiProfile = '/profiles';
  private config = 'https://localhost:44375/api';

  @Output() getAvatar: EventEmitter<any> = new EventEmitter();
  constructor(
    private http : HttpClient,
    private  authenticationService :AuthenticationService,
  ) { }

  changeAvatar(image: File):Observable<Avatar>  {
    this.setTokenToHeader();
    const formData:  FormData = new FormData();
    formData.append('file',image);
    return this.http.post<Avatar>(`${this.config}${this.apiProfile}/avatar`, formData, httpOptions);
  }

  setTokenToHeader(){
    let tokenString :string;
    if(this.authenticationService.hasToken()){
      tokenString = this.authenticationService.getToken();
      httpOptions.headers =
      httpOptions.headers.set('Authorization', `Bearer ${tokenString}`);
      console.log("this.tokenString 111: " + tokenString);
    }
  }

  /** POST: add a new comment to the database */
  /*addComment(objective: Objective): Observable<Objective>{
  this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
  this.setTokenToHeader();
  return this.http.post<Objective>(`${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiObjective}`, objective, httpOptions)
    .pipe(
      catchError(this.handleErrorService.handleError('addObjective',objective))
    );
  }*/ 


}
