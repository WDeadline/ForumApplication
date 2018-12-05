import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import {ProfileUpdateService} from './profile-update.service';
import {User} from '../model/user';
import {CurrentUserInfo} from '../../authentication/model/current-user-info';
import {Skill} from '../model/skill';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { error } from 'util';
class ImageSnippet {
  constructor(public src: string, public file: File) {}
}
@Component({
  selector: 'app-profile-update',
  templateUrl: './profile-update.component.html',
  styleUrls: ['./profile-update.component.css']
})
export class ProfileUpdateComponent implements OnInit {

  selectedFile: ImageSnippet;
  imageURL: string = "assets/img/default.png";
  fileToUpload : File = null;
  constructor(
    private profileUpdateService : ProfileUpdateService,
  ) { }

  ngOnInit() {
  }


  uploadPhoto(e:any){
    this.fileToUpload = e.files[0];
    const file: File = e.files[0];
    var reader = new FileReader();
    reader.readAsDataURL(this.fileToUpload);
    reader.onload = (event: any) => {
      this.imageURL = event.target.result;
      this.selectedFile = new ImageSnippet(event.target.result, file);
      this.profileUpdateService.changeAvatar(this.selectedFile.file).subscribe(
        data => {
          console.log("thanh cong1");
          console.log(data);
          console.log("thanh cong2");
          },
        error => {
          console.log("that bai1");
          console.log(error);
          console.log("that bai2");
        }
      )
    }
    
    
  }


}
