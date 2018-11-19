import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile-update',
  templateUrl: './profile-update.component.html',
  styleUrls: ['./profile-update.component.css']
})
export class ProfileUpdateComponent implements OnInit {

  imageUpload: any;
  imageURL: any;
  constructor() { }

  ngOnInit() {
  }


  handleImageUpload(e) {
    /*if(e.target.files && e.target.files.length > 0) {
      this.imageUpload = e.target.files[0];
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.imageURL = event.target.result;
      }
      reader.readAsDataURL(this.imageUpload);
  
      let body = new FormData();
      body.append("file", this.imageUpload);
      this.profileService.changeAvatar(body).subscribe(
        data => {
          toast('Update avatar successfully', 1500);
          let user: User = new User();
          user = data;
          var userInfo_json = localStorage.getItem('userInfo');
          var userInfo: UserInfo = JSON.parse(userInfo_json);
          userInfo.currentUser.avatar = user.avatar;
          this.profileService.setUserInfo(userInfo);
        }, error => console.log(error),
        () => {
          this.loadInfoMentor();
        }
      );
    }*/

  }

}
