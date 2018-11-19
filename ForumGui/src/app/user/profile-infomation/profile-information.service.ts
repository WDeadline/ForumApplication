import { Injectable } from '@angular/core';
import {User} from '../../user/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {AuthenticationService} from '../../authentication/service/authentication.service';

import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service'; 
@Injectable({
  providedIn: 'root'
})
export class ProfileInformationService {
  private config = 'https://localhost:44375/api';
  private apiProfileInfo = '/tests/infomations';
  user : User;
  constructor(
    private http: HttpClient,
    private authenticationService: AuthenticationService,
    private handleErrorService: HandleErrorService,
  ) { }

  getInformation(){
    return this.http.get<User[]>(`${this.config}${this.apiProfileInfo}`)
      .pipe(
        catchError(this.handleErrorService.handleError('getQuestions',[]))
      ) 
  }

  


}
