import { Component, OnInit } from '@angular/core';
import{ProfileInformationService} from './profile-information.service';
import {Information} from '../model/information';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  information : Information[] = [];
  editInformation: Information = new Information();
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
    this.getCurrentUserId();
    this.getInformation();
    this.informationFormEdit = this.formBuilder.group({
      fullNameEdit : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      addressEdit : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      numberPhoneEdit :['',[Validators.required,Validators.pattern('^([0-9]*)$')]], 
    }); 
  }

  get f() { return this.informationFormEdit.controls; }

  getInformation(){
    this.profileInformationService.getInformationOfCurrentUser(this.currentUserId)
      .subscribe(data => {
        this.information.push(data);
      });
  }

  edit(information: Information){
    if(information){
      this.editInformation = information;
      this.setValidator(this.editInformation.fullName, this.editInformation.address,this.editInformation.phoneNumber);
      if(this.editInformation != null && this.editInformation.gender != null){
        this.gender = this.editInformation.gender;
      }
      this.editBirthDay = new Date(this.editInformation.dateOfBirth);

    }
  }


  updateInfomation(){
    this.submitted = true;
    if (this.informationFormEdit.invalid) {
      return;
    }
    let fullName = this.f.fullNameEdit.value;
    if(this.selectedGender != null){
      this.editInformation.gender = this.selectedGender;
      this.gender = this.selectedGender;
    }
     
    let phoneNumber = this.f.numberPhoneEdit.value;
    let address = this.f.addressEdit.value;

    this.editInformation.fullName = fullName;
    this.editInformation.userId = this.authenticationService.getCurrentUser().id;
    if(this.birthDay){
      this.editInformation.dateOfBirth = this.birthDay;
    }
    this.editInformation.phoneNumber = phoneNumber;
    this.editInformation.address = address;
    this.editInformation.updationTime = new Date();

    this.profileInformationService.updateInformation(this.editInformation)
      .subscribe(data => {
        console.log("updateInformation successful");
        this.isEdit = false;
    })
  }

  setValidator(fullName: string ,address : string, numberPhone:string){
    this.informationFormEdit = this.formBuilder.group({
      fullNameEdit : [ fullName ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      addressEdit : [address ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      numberPhoneEdit :[numberPhone,[Validators.required,Validators.pattern('^([0-9]*)$')]], 
    }); 
  }

  getCurrentUserId(){
    this.currentUserId = this.authenticationService.getCurrentUser().id;
    console.log("getCurrentUserId: "+ this.currentUserId);
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
