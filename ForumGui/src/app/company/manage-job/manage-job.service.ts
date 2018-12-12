import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Work} from '../../job/work';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class ManageJobService {

  private apiWork = '/Works';
  private apiCompany = '/companies';
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

  /* GET: get Work of all users from server */ // api/works/companies/companyId
  getWorks(): Observable<Work[]>{
    this.anthenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    const url = `${this.config}${this.apiWork}${this.apiCompany}/${this.anthenticationService.currentUserInfo.id}`;
    return this.http.get<Work[]>(`${this.config}${this.apiWork}`)
      .pipe(
        catchError(this.handleErrorService.handleError('getWorks',[]))
      )
  }

  /** POST: add a new job to the database */ //api/Works
  addWork(work: Work): Observable<Work>{
    const url = `${this.config}${this.apiWork}`;
    this.setTokenToHeader();
    return this.http.post<Work>(url, work, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addWork', work))
      );
  }

  /*PUT: update the work on the server. Return the updated object upon success */
  updateWork(work: Work): Observable<Work>{
    const url = `${this.config}${this.apiWork}/${work.id}`;
    this.setTokenToHeader();
    return this.http.put<Work>(url, work, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateWork',work))
      );
  }

  /** DELETE: delete the job from the server */
  deleteWork (work: Work): Observable<{}>{
    const url = `${this.config}${this.apiWork}/${work.id}`;
    this.setTokenToHeader();
    return this.http.delete(url, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('deleteWork'))
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
