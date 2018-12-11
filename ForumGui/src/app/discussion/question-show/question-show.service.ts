import { Injectable } from '@angular/core';
import {Question} from '../model/question';
import {Comment} from '../model/comment';
import {Anwer} from '../model/anwer';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { HandleErrorService } from '../../handle-error.service';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class QuestionShowService {
  private apiQuestion ='/questions';
  private apiAnswers ='/answers';
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,

  ) { }


  getQuestionById(id: string): Observable<Question>{
      const url =`${this.config}${this.apiQuestion}/${id}`;
      return this.http.get<Question>(url,httpOptions)
      .pipe(
        //catchError(this.handleErrorService.handleError('getQuestionById'))
      )

    }

  getAnwerByQuestion(id: string): Observable<Anwer[]>{
    const url =`${this.config}${this.apiQuestion}/${id}${this.apiAnswers}`;
    return this.http.get<Anwer[]>(url)
      .pipe(
        catchError(this.handleErrorService.handleError('getAnwerByQuestion',[]))
      );

  }

  /* POST: add a new anwer to the database */// POST: api/questions/questionId/answers
  addAnswer(anwer: Anwer,questionId: string): Observable<Anwer>{
    this.setTokenToHeader();
    return this.http.post<Anwer>(`${this.config}${this.apiQuestion}/${questionId}${this.apiAnswers}`, anwer, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addAnswer',anwer))
      );
    }

  /*PUT: update the anwer on the server. Return the updated object upon success */
  updateAnswer(anwer: Anwer,question: Question): Observable<Anwer>{
    const url = `${this.config}${this.apiQuestion}/${question.id}${this.apiAnswers}/${anwer.id}`;
    console.log("edit url:" + url);
    this.setTokenToHeader();
    return this.http.put<Anwer>(url,anwer,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateAnswer',anwer))
      );
  }

  /** DELETE: delete the anwer from the server */
  deleteAnwer(anwer: Anwer,question: Question) :  Observable<{}>{
    const url = `${this.config}${this.apiQuestion}/${question.id}${this.apiAnswers}/${anwer.id}`;
    this.setTokenToHeader();
    return this.http.delete(url, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('deleteAnwer'))
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
