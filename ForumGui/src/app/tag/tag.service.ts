import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HandleErrorService } from '../handle-error.service';

import { HttpClient } from '@angular/common/http';
import {Tag} from '../discussion/model/tag';
@Injectable({
  providedIn: 'root'
})
export class TagService {

  private apiTag = '/tags';  // api/users
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
  ) { }

    /* GET: get all user from server */
    getAllTag(): Observable<Tag[]>{ 
      const url = `${this.config}/questions${this.apiTag}`;
      return this.http.get<Tag[]>(url)
        .pipe(
          catchError(this.handleErrorService.handleError('getAllTag',[]))
        )
    }


}
