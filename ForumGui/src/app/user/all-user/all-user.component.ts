import { Component, OnInit } from '@angular/core';
import {User} from '../model1/user';
import {AllUserService} from './all-user.service';
@Component({
  selector: 'app-all-user',
  templateUrl: './all-user.component.html',
  styleUrls: ['./all-user.component.css']
})
export class AllUserComponent implements OnInit {

  users : User[];
  usersView : User[];
  imageURL: string = "assets/img/default.png";
  constructor(
    private allUserService : AllUserService,
  ) { }

  ngOnInit() {
    this.getAllUser();
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
