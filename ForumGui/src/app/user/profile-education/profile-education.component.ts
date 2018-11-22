import { Component, OnInit } from '@angular/core';
import {ProfileEducationService} from './profile-education.service';
import {Objective} from '../model/objective';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import { Education } from '../model/educaton';
import {formatDate } from '@angular/common';
@Component({
  selector: 'app-profile-education',
  templateUrl: './profile-education.component.html',
  styleUrls: ['./profile-education.component.css']
})
export class ProfileEducationComponent implements OnInit {

  isEdit = false;
  isAdd =false;
  educationFormAdd: FormGroup;
  educationFormEdit: FormGroup;
  submittedAdd = false;
  submittedEdit = false;
  newEducation :Education = new Education();
  editEducation : Education = new Education();
  educations : Education[];
  startDateAdd : Date ;
  endDateAdd : Date ;
  startDateEdit : Date;
  endDateEdit : Date;
  bsValue : Date = new Date();
  
  errAdd:string;
  constructor(
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private profileEducationService :ProfileEducationService,
  ) { }

  ngOnInit() {
    this.getObjectives();
    this.educationFormAdd = this.formBuilder.group({
      universityAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
      majorAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],        
      GPAAdd :  ['', Validators.required], 
    });
  }


  get formAdd() { return this.educationFormAdd.controls; }
  
  get formEdit(){return this.educationFormEdit.controls; }
  //get tat ca Objective của các users
  getObjectives(){
    this.profileEducationService.getEducations()
      .subscribe(data => this.educations = data);
  }

  addEducation(){
    this.submittedAdd = true;

    if(this.educationFormAdd.invalid){
      return;
    }

    if(this.endDateAdd <= this.startDateAdd){
      this.errAdd ="The end date must be greater than the start day";
      return;
    }

    let userId = this.authenticationService.getCurrentUser().id;
    let university = this.formAdd.universityAdd.value;
    let major = this.formAdd.majorAdd.value;
    let GPA = this.formAdd.GPAAdd.value;
    

    this.newEducation.userId = userId;
    this.newEducation.university = university;
    this.newEducation.major = major;
    this.newEducation.gpa = GPA;

    if(this.startDateAdd) {
      this.newEducation.startTime = this.startDateAdd;
    }else{
      //this.newEducation.startTime = new Date();
    }

    if(this.endDateAdd) {
      this.newEducation.endTime = this.endDateAdd;
    }else{
      //this.newEducation.startTime = new Date();
    }
    //con UpdationTime

    this.profileEducationService.addEducation(this.newEducation)
    .subscribe(data => {
      console.log("add Objective sussectfule");
      this.educations.push(data);
      this.isAdd = false;
    });

  }

  updateEducation(){
    this.submittedEdit = true;
    if(this.educationFormEdit.invalid){
      return;
    }

    if(this.endDateEdit <= this.startDateEdit){
      this.errAdd ="The end date must be greater than the start day";
      return;
    }

    let userId = this.authenticationService.getCurrentUser().id;
    let university = this.formEdit.universityEdit.value;
    let major = this.formEdit.majorEdit.value;
    let GPA = this.formEdit.GPAEdit.value;
    
    this.editEducation.userId = userId;
    this.editEducation.university = university;
    this.editEducation.major = major;
    this.editEducation.gpa = GPA;

    if(this.startDateEdit) {
      this.editEducation.startTime = this.startDateEdit;
    }else{
      //this.newEducation.startTime = new Date();
    }

    if(this.endDateEdit) {
      this.editEducation.endTime = this.endDateEdit;
    }else{
      //this.newEducation.startTime = new Date();
    }

    // this.profileEducationService.addEducation(this.newEducation)
    // .subscribe(data => {
    //   console.log("add Objective sussectfule");
    //   this.educations.push(data);
    //   this.isAdd = false;
    // });

    this.profileEducationService.updateEducation(this.editEducation)
    .subscribe(data => {
      console.log("edit education succesfull");
      this.isEdit = false;
    })
  }

  deleteEducation(education: Education){
    this.educations = this.educations.filter(h => h !== education);
    this.profileEducationService.deleteEducation(education.id)
      .subscribe(data => {console.log("deleteEducation successful")})
  }

  onValueChangeStartAdd(value: Date){
    this.startDateAdd = value;
  }
  onValueChangeEndAdd(value: Date){
    this.endDateAdd = value;
  }

  onValueChangeStartEdit(value: Date){
    console.log("timest " + value);
    this.startDateEdit = value;
  }
  onValueChangeEndEdit(value: Date){
    console.log("timest " + value);
    this.endDateEdit = value;
  }

  edit(education:Education){
    this.editEducation = education;
    this.startDateEdit = education.startTime;
    this.endDateEdit = education.endTime;
    let temp = new Date();
    console.log("time" + temp);
    console.log("start tiem" + this.editEducation.startTime);
    this.editForm(this.editEducation.university, this.editEducation.major, this.editEducation.gpa);
  }

  getStartDate(){
    var today = new Date();
    console.log(today);
    return today;
  }

  editForm(university: string, major:string , gpa :number,){
    this.educationFormEdit = this.formBuilder.group({
      universityEdit : [university, [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
      majorEdit : [major, [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],        
      GPAEdit :  [gpa, Validators.required], 
    }); 
  }
}
