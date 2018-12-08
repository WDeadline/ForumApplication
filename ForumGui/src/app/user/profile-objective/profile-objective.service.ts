import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { HandleErrorService } from '../../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Objective} from '../model1/objective';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class ProfileObjectiveService {
  private apiUser = '/users'; //api/users/userId/objectives
  private apiObjective = '/objectives';
  private config = 'https://localhost:44375/api';
  objectives : Objective[];
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

    /** POST: add a new Objective to the database */
    addObjective(objective: Objective): Observable<Objective>{
      this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
      this.setTokenToHeader();
      return this.http.post<Objective>(`${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiObjective}`, objective, httpOptions)
        .pipe(
          catchError(this.handleErrorService.handleError('addObjective',objective))
        );
    }

    /* GET: get Objectives of all users from server */
    getObjectives(): Observable<Objective[]>{
      this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
      this.setTokenToHeader();
      return this.http.get<Objective[]>(`${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiObjective}`, httpOptions)
        .pipe(
          catchError(this.handleErrorService.handleError('getObjectives',[]))
        )
    }

    /*PUT: update the Object on the server. Return the updated object upon success */
    updateObjective(objective: Objective): Observable<Objective>{
      this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
      const url = `${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiObjective}/${objective.id}`;
      console.log("edit url:" + url);
      this.setTokenToHeader();
      return this.http.put<Objective>(url,objective,httpOptions)
        .pipe(
          catchError(this.handleErrorService.handleError('updateHero',objective))
        );
    }

      /** DELETE: delete the objective from the server */
    deleteObject(id: string) :  Observable<{}>{
      this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
      const url = `${this.config}${this.apiUser}/${this.anthenticationService.currentUserInfo.id}${this.apiObjective}/${id}`;
      this.setTokenToHeader();
      return this.http.delete(url, httpOptions)
        .pipe(
          catchError(this.handleErrorService.handleError('deleteObject'))
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
