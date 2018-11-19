import { Component, OnInit } from '@angular/core';
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

  isLoggedIn$: Observable<boolean>;

  ngOnInit() { 
    this.isLoggedIn$ = this.authentication.isLoggedIn();
    this.getUserInfo();
  }

  
  getUserInfo() {
    this.authentication.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
  }

  onLogout(){
    this.authentication.logOut();
    this.authentication.currentUserInfo = null;
  }

  logOut(){
    this.authentication.logOut();
  }

  getUserRole(): string {
    return this.authentication.getUserRoleFromLocalStorage();
  }


}
