import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Activity} from '../model1/activity';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class ProfileActivityService {

  private apiUser ='/users';
  private apiActivity = '/activities';
  private config = 'https://localhost:44375/api';
  
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

  /* GET: get activity of user from server */  //api/users/userId/activities
  getActivities():Observable<Activity[]>{ 
    this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    const url = `${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiActivity}`;
    this.setTokenToHeader();
    return this.http.get<Activity[]>(url,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('getActivities',[]))
      )
  }

  
  /* GET: get activity of user from server */  //api/users/userId/activities
  getActivitiesById(id: string):Observable<Activity[]>{ 
    const url = `${this.config}${this.apiUser}/${id}${this.apiActivity}`;
    this.setTokenToHeader();
    return this.http.get<Activity[]>(url,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('getActivities',[]))
      )
  }

  /** POST: add a new activity to the database */
  addActivity(activity: Activity): Observable<Activity>{
    this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    const url = `${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiActivity}`
    this.setTokenToHeader();
    return this.http.post<Activity>(url, activity, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addActivity', activity))
      );
  }

  /*PUT: update the activity on the server. Return the updated object upon success */
  updateActivity(activity: Activity): Observable<Activity>{
    this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    const url = `${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo}${this.apiActivity}/${activity.id}`;
    this.setTokenToHeader();
    return this.http.put<Activity>(url, activity, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateActivity',activity))
      );
  }

  /** DELETE: delete the activity from the server */
  deleteActivity(activity: Activity): Observable<{}>{
    this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    const url = `${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiActivity}/${activity.id}`;
    this.setTokenToHeader();
    return this.http.delete(url, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('deleteActivity'))
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
}
