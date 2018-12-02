import { Injectable } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service'; 
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HandleErrorService } from '../../handle-error.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Skill} from '../model/skill';

const httpOptions = {
  headers: new HttpHeaders({ 
    'Content-Type': 'application/json'
  })
};
@Injectable({
  providedIn: 'root'
})
export class ProfileSkillService {

  private apiSkill = '/Skills';
  private config = 'https://localhost:44375/api';
  constructor(
    private handleErrorService: HandleErrorService,
    private http: HttpClient,
    private anthenticationService :AuthenticationService,
  ) { }

  /* GET: get Skill of all users from server */
  getSkills():Observable<Skill[]>{ 
    const url = `${this.config}${this.apiSkill}`;
    this.setTokenToHeader();
    return this.http.get<Skill[]>(url,httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('getSkills',[]))
      )
  }

  /** POST: add a new skill to the database */
  addSkill(skill: Skill): Observable<Skill>{
    const url = `${this.config}${this.apiSkill}`
    this.setTokenToHeader();
    return this.http.post<Skill>(url, skill, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('addSkill', skill))
      );
  }

  /*PUT: update the skill on the server. Return the updated object upon success */
  updateSkill(skill: Skill): Observable<Skill>{
    const url = `${this.config}${this.apiSkill}/${skill.id}`;
    this.setTokenToHeader();
    return this.http.put<Skill>(url, skill, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('updateSkill',skill))
      );
  }

  /** DELETE: delete the skill from the server */
  deleteSkill(skill: Skill): Observable<{}>{
    const url = `${this.config}${this.apiSkill}/${skill.id}`;
    this.setTokenToHeader();
    return this.http.delete(url, httpOptions)
      .pipe(
        catchError(this.handleErrorService.handleError('deleteSkill'))
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
