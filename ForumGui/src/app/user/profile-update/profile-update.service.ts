import { Injectable,EventEmitter,Output } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {User} from '../user';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import {CurrentUserInfo} from '../../authentication/model/current-user-info';
@Injectable({
  providedIn: 'root'
})
export class ProfileUpdateService {

  @Output() getAvatar: EventEmitter<any> = new EventEmitter();
  public header = new HttpHeaders({
    'Content-Type':'application/json',
    'Authorization': this.authenticationService.getToken()
  });
  constructor(
    private http : HttpClient,
    private  authenticationService :AuthenticationService,
  ) { }

  getInfoUser(id: number): Observable<User> {
    return this.http.get<User>(`api/${id}`, {headers: this.header});
  }
  changeAvatar(body: any): Observable<User> {
    return this.http.put<User>("api", body);
  }

  setUserInfo(currentUser: CurrentUserInfo) {
    this.getAvatar.emit(currentUser);
  }
}
