import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import {AuthenticationService} from '../authentication/service/authentication.service';
import { Work } from './work';
import { HandleErrorService } from '../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class JobService {
  private apiWork = '/Works'; // api/Works
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

  /* GET: get Work of all users from server */
  getWorks(): Observable<Work[]>{
    return this.http.get<Work[]>(`${this.config}${this.apiWork}`)
      .pipe(
        catchError(this.handleErrorService.handleError('getWorks',[]))
      )
  }



}
