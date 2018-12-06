import { Component, OnInit } from '@angular/core';
import{ProfileInformationService} from './profile-information.service';
import {Information} from '../model/information';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {User} from  '../model1/user';
@Component({
  selector: 'app-profile-infomation',
  templateUrl: './profile-infomation.component.html',
  styleUrls: ['./profile-infomation.component.css']
})
export class ProfileInfomationComponent implements OnInit {

  submitted = false;
  informationFormEdit: FormGroup;
  isEdit = false;
  currentUserId : string;
  information : User[] = [];

  editInformation: User = new User();
  gender : boolean = false;
  selectedGender : boolean;
  birthDay : Date;
  editBirthDay : Date;
  constructor(
    private authenticationService : AuthenticationService,
    private profileInformationService : ProfileInformationService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.currentUserId = this.authenticationService.getCurrentUser().id;
    this.getInformation();
    this.informationFormEdit = this.formBuilder.group({
      nameEdit : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      addressEdit : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      numberPhoneEdit :['',[Validators.required,Validators.pattern('^([0-9]*)$')]], 
      positionEdit : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
    }); 
  }

  get f() { return this.informationFormEdit.controls; }

  getInformation(){
    this.profileInformationService.getInformationOfCurrentUser(this.currentUserId)
      .subscribe(data => {
        this.information.push(data);
      });
  }

  edit(information: User){
    if(information){
      this.editInformation = information;
      this.setValidator(this.editInformation.name, this.editInformation.address,this.editInformation.phoneNumber, this.editInformation.position);
      if(this.editInformation != null && this.editInformation.gender != null){
        this.gender = this.editInformation.gender;
      }
      this.editBirthDay = new Date(this.editInformation.birthday);

    }
  }


  updateInfomation(){
    this.submitted = true;
    if (this.informationFormEdit.invalid) {
      return;
    }
    let name = this.f.nameEdit.value;
    if(this.selectedGender != null){
      this.editInformation.gender = this.selectedGender;
      this.gender = this.selectedGender;
    }
     
    let phoneNumber = this.f.numberPhoneEdit.value;
    let address = this.f.addressEdit.value;
    let position = this.f.positionEdit.value;

    this.editInformation.name = name;
    if(this.birthDay){
      this.editInformation.birthday = this.birthDay;
    }
    this.editInformation.phoneNumber = phoneNumber;
    this.editInformation.address = address;
    this.editInformation.position = position;

    this.profileInformationService.updateInformation(this.editInformation)
      .subscribe(data => {
        console.log("updateInformation successful");
        this.isEdit = false;
    })
  }

  setValidator(name: string ,address : string, numberPhone:string, position:string ){
    this.informationFormEdit = this.formBuilder.group({
      nameEdit : [ name ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      addressEdit : [address ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      numberPhoneEdit :[numberPhone,[Validators.required,Validators.pattern('^([0-9]*)$')]], 
      positionEdit : [ position ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
    }); 
  }



  handelRadioMale(event){
    if(event.target.value == "true"){
      this.selectedGender = true;
    }   
  }

  handelRadioFemale(event){
    if(event.target.value == "false"){
      this.selectedGender = false;
    }
  }

  onValueChangeEdit(value: Date){
    this.birthDay = value;
  }
}
