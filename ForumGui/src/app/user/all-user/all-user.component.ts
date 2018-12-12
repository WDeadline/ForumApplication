import { Component, OnInit } from '@angular/core';
import {User} from '../model1/user';
import {AllUserService} from './all-user.service';
import { Observable } from 'rxjs';
import {AuthenticationService} from '../../authentication/service/authentication.service';
@Component({
  selector: 'app-all-user',
  templateUrl: './all-user.component.html',
  styleUrls: ['./all-user.component.css']
})
export class AllUserComponent implements OnInit {

  users : User[];
  usersView : User[];
  imageURL: string = "assets/img/default.png";
  isLoggedIn$: Observable<boolean>;
  constructor(
    private allUserService : AllUserService,
    private authentication : AuthenticationService,
  ) { }

  ngOnInit() {
    this.isLoggedIn$ = this.authentication.isLoggedIn();
    this.getAllUser();
  }

  getUserRole(): string {
    console.log("userRole: "+this.authentication.getUserRoleFromLocalStorage());
    return this.authentication.getUserRoleFromLocalStorage();
  }

  getAllUser(){
    this.allUserService.getAllUser()
      .subscribe(data => {
        this.users = data;
        this.usersView = data;
        console.log("getAllTag succesfull");
      })
  }

  onSearchChange(searchValue : string){
    if(searchValue != ''){
      this.usersView = this.users.filter(u => u.name.toLowerCase().indexOf(searchValue.toLowerCase())> -1);
    }else{
      this.usersView = this.users;
    }
    console.log(searchValue);
  }


}
