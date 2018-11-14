import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Test} from './test';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class TestService {

  constructor(
    private http: HttpClient
  ) { }

  
    /** GET heroes from the server */
    getTests(): Observable<Test[]>{
      let currentUser = JSON.parse(localStorage.getItem('currentUser'));
      if (currentUser && currentUser.token) {
        httpOptions.headers =
        httpOptions.headers.set('Authorization', `Bearer ${currentUser.token}`);
        console.log("currentUser.token" + currentUser.token);
      }
      else{
        console.log("token not exit");
      }
      return this.http.get<Test[]>(`https://192.168.1.7:5001/api/tests/teacher`,httpOptions);
    }
}
