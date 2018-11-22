import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import {Education} from '../model/educaton';
import { HandleErrorService } from '../../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ProfileEducationService {

  private apiEducation = '/educations';
  private config = 'https://localhost:44375/api';

  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

    /** POST: add a new Education to the database */
    addEducation(education: Education):Observable<Education>{
      const url = `${this.config}${this.apiEducation}`;
      this.setTokenToHeader();
      return this.http.post<Education>(url,education,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addEducation',education))
      );
    }

    /*PUT: update the Education on the server. Return the updated object upon success */
    updateEducation(education: Education): Observable<Education>{
      const url = `${this.config}${this.apiEducation}/${education.id}`;
      this.setTokenToHeader();
      return this.http.put<Education>(url, education, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateEducation',education))
      );
    }

    /** DELETE: delete the education from the server */
    deleteEducation(id:string) : Observable<{}>{
      const url = `${this.config}${this.apiEducation}/${id}`;
      this.setTokenToHeader();
      return this.http.delete(url,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('deleteEducation'))
      );
    }

    /* GET: get Educations of all users from server */
    getEducations():Observable<Education[]>{
      const url =  `${this.config}${this.apiEducation}`;
      this.setTokenToHeader();
      return this.http.get<Education[]>(url,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('getEducations',[]))
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
