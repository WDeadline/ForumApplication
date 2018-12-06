import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Experience} from '../model1/experience';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class ProfileExperienceService {

  private apiUser = '/users'; //api/users/userId/experiences
  private apiExperience = '/experiences';
  private config = 'https://localhost:44375/api';
  private currentUserId = this.anthenticationService.getCurrentUser().id;
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

    /* GET: get Experience of user from server */
    getExperiences(): Observable<Experience[]>{
      const url = `${this.config}${this.apiUser}/${this.currentUserId}${this.apiExperience}`;
      this.setTokenToHeader();
      return this.http.get<Experience[]>(url,httpOptions )
        .pipe(
          catchError(this.handleErrorService.handleError('getExperiences',[]))
        )
    }
    /** POST: add a new Experience to the database */
    addExperience(experience: Experience): Observable<Experience>{
      const url = `${this.config}${this.apiUser}/${this.currentUserId}${this.apiExperience}`
      this.setTokenToHeader();
      return this.http.post<Experience>(url, experience, httpOptions)
        .pipe(
          catchError(this.handleErrorService.handleError('addExperience', experience))
        );
    }

    /*PUT: update the experience on the server. Return the updated object upon success */
    updateExperience(experience: Experience): Observable<Experience>{
      const url = `${this.config}${this.apiUser}/${this.currentUserId}${this.apiExperience}/${experience.id}`;
      this.setTokenToHeader();
      return this.http.put<Experience>(url, experience, httpOptions)
        .pipe(
          catchError(this.handleErrorService.handleError('updateExperience',experience))
        );
    }

    /** DELETE: delete the experience from the server */
    deleteExperience(experience: Experience): Observable<{}>{
      const url = `${this.config}${this.apiUser}/${this.currentUserId}${this.apiExperience}/${experience.id}`;
      this.setTokenToHeader();
      return this.http.delete(url, httpOptions)
        .pipe(
          catchError(this.handleErrorService.handleError('deleteExperience'))
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
