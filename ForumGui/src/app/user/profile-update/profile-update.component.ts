import { Component, OnInit } from '@angular/core';
import {ProfileUpdateService} from './profile-update.service';
import {User} from '../model/user';
import {CurrentUserInfo} from '../../authentication/model/current-user-info';
@Component({
  selector: 'app-profile-update',
  templateUrl: './profile-update.component.html',
  styleUrls: ['./profile-update.component.css']
})
export class ProfileUpdateComponent implements OnInit {

  imageUpload: any;
  imageURL: any;
  constructor(
    private profileUpdateService : ProfileUpdateService,
  ) { }

  ngOnInit() {
  }


  handleImageUpload(e) {
    if(e.target.files && e.target.files.length > 0) {
      this.imageUpload = e.target.files[0];
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.imageURL = event.target.result;
      }
      reader.readAsDataURL(this.imageUpload);
  
      let body = new FormData();
      body.append("file", this.imageUpload);

     this.profileUpdateService.changeAvatar(body).subscribe(
        data => {
          //toast('Update avatar successfully', 1500);
          let user: User = new User();
          user = data;
          var currentUser: CurrentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
          //currentUser.avatar = user.avatar;
          this.profileUpdateService.setUserInfo(currentUser);
        }, error => console.log(error),
        () => {
          //this.loadInfoMentor();
        }
      )
    }

  }

}
