import { Component, OnInit } from '@angular/core';
import {ProfileEducationService} from './profile-education.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import { Education } from '../model1/education';

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
  startDateAdd : Date = new Date();
  endDateAdd : Date = new Date();
  startDateEdit : Date= new Date();
  endDateEdit : Date= new Date();
  bsValue : Date = new Date();
  
  isErrorDateAdd: boolean = false;
  isErrorDateEdit: boolean = false;
  isErrorStartDayAdd: boolean = false;
  isErrorStartDayEdit: boolean = false;

  errEdit:string;
  constructor(
    private formBuilder: FormBuilder,
    private profileEducationService :ProfileEducationService,
  ) { }

  ngOnInit() {
    this.getObjectives();
    this.setValidateAdd();
  }


  get formAdd() { return this.educationFormAdd.controls; }
  
  get formEdit(){return this.educationFormEdit.controls; }
  
  //get Objectives of user
  getObjectives(){
    this.profileEducationService.getEducations()
      .subscribe(data => this.educations = data);
  }

  addEducation(){
    this.submittedAdd = true;
    var isError = false;
    if(this.startDateAdd < new Date(1990,1,1) || this.startDateAdd > new Date()){
      this.isErrorStartDayAdd = true;
      isError = true;
    }
    if(this.endDateAdd < this.startDateAdd){
      this.isErrorDateAdd = true;
      isError = true;
    }

    if(this.educationFormAdd.invalid){
      isError = true;
    }

    if(isError){
      return;
    }

    
    this.newEducation.university = this.formAdd.universityAdd.value;
    this.newEducation.major = this.formAdd.majorAdd.value;
    this.newEducation.gpa = this.formAdd.GPAAdd.value;
    this.newEducation.startTime = this.startDateAdd;
    this.newEducation.endTime = this.endDateAdd;

    this.profileEducationService.addEducation(this.newEducation)
    .subscribe(data => {
      console.log("add Objective sussectfule");
      this.educations.push(data);
      this.setValidateAdd();
      this.isAdd = false;
      this.submittedAdd = false;
    });

  }

  updateEducation(){
    this.submittedEdit = true;
    var isError = false;
    if(this.startDateEdit < new Date(1990,1,1) || this.startDateEdit > new Date()){
      this.isErrorStartDayEdit = true;
      isError = true;
    }
    if(this.endDateEdit < this.startDateEdit){
      this.isErrorDateEdit = true;
      isError = true;
    }
    if(this.educationFormEdit.invalid){
      isError = true;
    }

    if(isError){
      return;
    }

    this.editEducation.university = this.formEdit.universityEdit.value;
    this.editEducation.major = this.formEdit.majorEdit.value;
    this.editEducation.gpa = this.formEdit.GPAEdit.value;
    this.editEducation.startTime = this.startDateEdit;
    this.editEducation.endTime = this.endDateEdit;

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
    this.isErrorDateAdd = this.submittedAdd && this.endDateAdd < this.startDateAdd;
    this.isErrorStartDayAdd = this.submittedAdd && ( this.startDateAdd < new Date(1990,1,1) || this.startDateAdd > new Date());
  }
  onValueChangeEndAdd(value: Date){
    this.endDateAdd = value;
    this.isErrorDateAdd = this.submittedAdd && this.endDateAdd < this.startDateAdd;
  }

  onValueChangeStartEdit(value: Date){
    this.startDateEdit = value;
    this.isErrorDateEdit = this.submittedEdit && this.endDateEdit < this.startDateEdit;
    this.isErrorStartDayEdit = this.submittedEdit && (this.startDateEdit < new Date(1990,1,1) || this.startDateEdit > new Date());
  }
  onValueChangeEndEdit(value: Date){
    this.endDateEdit = value;
    this.isErrorDateEdit = this.submittedEdit && this.endDateEdit < this.startDateEdit;
  }

  edit(education:Education){
    this.editEducation = education;
    this.startDateEdit = new Date(education.startTime);
    this.endDateEdit = new Date(education.endTime);
    this.setValidateEdit(this.editEducation.university, this.editEducation.major, this.editEducation.gpa, new Date(this.editEducation.startTime), new Date(this.editEducation.endTime));
  }

  getStartDate(){
    var today = new Date();
    console.log(today);
    return today;
  }

  setValidateAdd(){
    this.educationFormAdd = this.formBuilder.group({
      universityAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
      majorAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],        
      GPAAdd :  ['', [Validators.required, Validators.min(0), Validators.max(4)]],
      startDateAdd :  ['', [Validators.required]],
      endDateAdd :  ['', [Validators.required]],
    });
  }
  setValidateEdit(university: string, major:string , gpa :number,startDate:Date, endDate:Date){
    this.educationFormEdit = this.formBuilder.group({
      universityEdit : [university, [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
      majorEdit : [major, [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],        
      GPAEdit :  [gpa, [Validators.required, Validators.min(0), Validators.max(4)]],
      fStartDateEdit :  [startDate, [Validators.required]],
      fEndDateEdit :  [endDate, [Validators.required]],
    }); 
  }
}
