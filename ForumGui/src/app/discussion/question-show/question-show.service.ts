import { Injectable } from '@angular/core';
import {Question} from '../model/question';
import {Comment} from '../model/comment';
import {Answer} from '../model/answer';
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
  private apiComment = '/comments';
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

  getAnwerByQuestion(id: string): Observable<Answer[]>{
    const url =`${this.config}${this.apiQuestion}/${id}${this.apiAnswers}`;
    return this.http.get<Answer[]>(url)
      .pipe(
        catchError(this.handleErrorService.handleError('getAnwerByQuestion',[]))
      );

  }

  /* POST: add a new anwer to the database */// POST: api/questions/questionId/answers
  addAnswer(content: string, questionId: string): Observable<Answer>{
    this.setTokenToHeader();
    return this.http.post<any>(`${this.config}${this.apiQuestion}/${questionId}${this.apiAnswers}`, {content : content}, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addAnswer', "add answer error"))
      );
    }

  /*PUT: update the anwer on the server. Return the updated object upon success */
  updateAnswer(anwer: Answer,question: Question): Observable<Answer>{
    const url = `${this.config}${this.apiQuestion}/${question.id}${this.apiAnswers}/${anwer.id}`;
    console.log("edit url:" + url);
    this.setTokenToHeader();
    return this.http.put<Answer>(url,anwer,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateAnswer',anwer))
      );
  }

  /** DELETE: delete the anwer from the server */
  deleteAnswer(questionId: string, answerId: string) :  Observable<{}>{
    const url = `${this.config}${this.apiQuestion}/${questionId}${this.apiAnswers}/${answerId}`;
    this.setTokenToHeader();
    return this.http.delete(url, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('deleteAnwer'))
      );
  }

  /* POST: add a new comment to the database */// POST: api/questions/questionId/answers
  addComment(questionId: string, answerId: string, content: string): Observable<Comment>{
    this.setTokenToHeader();
    return this.http.post<any>(`${this.config}${this.apiQuestion}/${questionId}${this.apiAnswers}/${answerId}${this.apiComment}`, {content : content}, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addAnswer', "add comment error"))
      );
  }

  /** DELETE: delete the comment from the server */
  deleteComment(questionId: string, answerId: string, commnetId: string) :  Observable<{}>{
    const url = `${this.config}${this.apiQuestion}/${questionId}${this.apiAnswers}/${answerId}${this.apiComment}/${commnetId}`;
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
