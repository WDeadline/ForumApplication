import { Component, OnInit } from '@angular/core';
import {AdminUserService} from './admin-user.service';
import {User} from '../../user/model1/user';
@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css']
})
export class AdminUserComponent implements OnInit {

  users : User[];
  stt : number = 0;
  isAdd = false;
  isEdit =false;
  constructor(
    private adminUserService:AdminUserService,
  ) { }

  ngOnInit() {
    this.getAllUsers();
  }


  getAllUsers(){
    this.adminUserService.getAllUser()
      .subscribe(data => {
        this.users = data;
      })
  }

}
