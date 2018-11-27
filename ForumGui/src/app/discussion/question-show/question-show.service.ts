import { Injectable } from '@angular/core';
import {Question} from '../model/question';
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
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,

  ) { }


  getQuestionById(id: string): Observable<Question>{
      const url =`${this.config}${this.apiQuestion}/${id}`;
      this.setTokenToHeader();
      return this.http.get<Question>(url,httpOptions)
      .pipe(
        //catchError(this.handleErrorService.handleError(''))
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
