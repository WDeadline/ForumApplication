import { Injectable } from '@angular/core';
import {Question} from '../model/question';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

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
export class QuestionMakeService {

  private apiGetAllTag ='/questions/tags';
  private apiQuestion = '/questions';
  private config = 'https://localhost:44375/api';

  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }



  /** POST: add a new Question to the database */
  addQuestion(question: Question): Observable<Question>{
    const url = `${this.config}${this.apiQuestion}`;
    this.setTokenToHeader();
    return this.http.post<Question>(url,question,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addQuestion',question))
      )
  }
    /* GET: get  all tags from server */
  getAllTag():Observable<string[]>{
    const url = `${this.config}${this.apiGetAllTag}`;
    return this.http.get<string[]>(url)
    .pipe(
      catchError(this.handleErrorService.handleError('getAllTag',[]))
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
