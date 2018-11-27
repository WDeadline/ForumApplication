import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Question } from '../discussion/model/question';
import { HandleErrorService } from '../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private apiGetQuestions = '/questions';
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
  ) { }

    /** GET questions from the server */
    getQuestions (): Observable<Question[]> {
      return this.http.get<Question[]>(`${this.config}${this.apiGetQuestions}`)
        .pipe(
          catchError(this.handleErrorService.handleError('getQuestions',[]))
        )   
    }

}
