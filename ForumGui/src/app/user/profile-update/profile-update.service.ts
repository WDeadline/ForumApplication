import { Injectable,EventEmitter,Output } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {User} from '../model/user';
import { catchError, map, tap } from 'rxjs/operators';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import {CurrentUserInfo} from '../../authentication/model/current-user-info';

const httpOptions = {
  headers: new HttpHeaders({ 
  })
};
@Injectable({
  providedIn: 'root'
})

export class ProfileUpdateService {

  private apiProfile = '/profiles';
  private config = 'https://localhost:44375/api';

  @Output() getAvatar: EventEmitter<any> = new EventEmitter();
  constructor(
    private http : HttpClient,
    private  authenticationService :AuthenticationService,
  ) { }

  /*getInfoUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.config}/${id}`, {headers: this.header});
  }*/  

  changeAvatar(image: File):Observable<Response>  {
    this.setTokenToHeader();
    const formData:  FormData = new FormData();
    formData.append('file',image);
    return this.http.post<Response>(`${this.config}${this.apiProfile}/avatar`, formData, httpOptions);
  }

  setUserInfo(currentUser: CurrentUserInfo) {
    this.getAvatar.emit(currentUser);
  }

  setTokenToHeader(){
    let tokenString :string;
    if(this.authenticationService.hasToken()){
      tokenString = this.authenticationService.getToken();
      httpOptions.headers =
      httpOptions.headers.set('Authorization', `Bearer ${tokenString}`);
      console.log("this.tokenString 111: " + tokenString);
    }
  }
}
