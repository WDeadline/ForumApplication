import { Component, OnInit } from '@angular/core';
import {CurrentUserInfo} from '../authentication/model/current-user-info';
import { Observable } from 'rxjs';
import {AuthenticationService} from '../authentication/service/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private authentication : AuthenticationService,
  ) { }

  currentUserInfo: CurrentUserInfo;
  isLoggedIn$: Observable<boolean>;
  isHidden : boolean;

  ngOnInit() { 
    this.isLoggedIn$ = this.authentication.isLoggedIn();
    console.log(this.authentication.isLoggedIn());
    this.getUserInfo();  
    this.isHidden = true;  
  }

  
  getUserInfo() {
    this.currentUserInfo = new CurrentUserInfo();
    this.currentUserInfo = JSON.parse(localStorage.getItem('userInfo'));
  }

  onLogout(){
    this.authentication.logOut();
  }

  logOut(){
    this.authentication.logOut();
  }

  getUserRole(): string {
    return this.authentication.getUserRoleFromLocalStorage();
  }


}
