import { Injectable } from '@angular/core';
import {Work} from '../job/work';
import {AuthenticationService} from '../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { HandleErrorService } from '../handle-error.service';
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
export class JobDetailService {

  private apiWork ='/Works'; //api/Works/id
  private config = 'https://localhost:44375/api';

  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

  getWorkById(id: string): Observable<Work>{
    const url =`${this.config}${this.apiWork}/${id}`;
    return this.http.get<Work>(url,httpOptions)
    .pipe(
      //catchError(this.handleErrorService.handleError('getQuestionById'))
    )

  }
}
