import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';

import { HttpClient } from '@angular/common/http';
import {User} from '../model1/user';

@Injectable({
  providedIn: 'root'
})
export class AllUserService {

  private apiUser = '/users';  // api/users
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
  ) { }
 

  
  /* GET: get all user from server */
  getAllUser(): Observable<User[]>{
    const url = `${this.config}${this.apiUser}`;
    return this.http.get<User[]>(url)
      .pipe(
        catchError(this.handleErrorService.handleError('getAllUser',[]))
      )
  }
}
