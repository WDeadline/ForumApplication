import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Experience} from '../model/experience';
import {ProfileExperienceService} from './profile-experience.service';
@Component({
  selector: 'app-profile-experience',
  templateUrl: './profile-experience.component.html',
  styleUrls: ['./profile-experience.component.css']
})
export class ProfileExperienceComponent implements OnInit {

  isEdit=false;
  isAdd = false;
  submittedAdd = false;
  submittedEdit = false;
  experienceAddForm: FormGroup;
  experienceEditForm: FormGroup;
  newExperience : Experience = new Experience();
  editExperience: Experience = new Experience();

  startDateAdd : Date = new Date();
  endDateAdd: Date = new Date();
  isErrorDateAdd : boolean = false; 
  isErrorDateEdit : boolean = false; 

  isErrorStartDayAdd: boolean = false;
  isErrorEndDayAdd: boolean = false;
  isErrorStartDayEdit: boolean = false;
  isErrorEndDayEdit: boolean = false;

  experiences : Experience[] = [];
  editStartDate = new Date();
  editEndDate = new Date();

  errEdit : string ;
  errAdd: string;
  constructor(
    private authenticationService : AuthenticationService,
    private formBuilder: FormBuilder,
    private profileExperienceService: ProfileExperienceService,
  ) { }

  ngOnInit() {
    this.getExperiences();
    this.setValidatorAdd();
  }

  get formAdd() { return this.experienceAddForm.controls; }

  get formEdit() { return this.experienceEditForm.controls; }

  getExperiences(){
    this.profileExperienceService.getExperiences()
      .subscribe(data => this.experiences = data);
  }

  addExperience(){

    this.submittedAdd = true;

    var isError = false;

    if(this.startDateAdd < new Date(1990,1,1) || this.startDateAdd > new Date()){
      this.isErrorStartDayAdd = true;
      isError = true;
    }

    if(this.endDateAdd < new Date(1990,1,1) || this.endDateAdd > new Date()){
      this.isErrorEndDayAdd = true;
      isError = true;
    }

    if(this.experienceAddForm.invalid){
      isError = true;
    }
    if(this.endDateAdd < this.startDateAdd){
      this.isErrorDateEdit = true;
      isError = true;
    }
    if(isError){
      return;
    }

    this.newExperience.workplace =  this.formAdd.workPlace.value;
    this.newExperience.position = this.formAdd.position.value;
    this.newExperience.description =  this.formAdd.description.value;
    this.newExperience.startTime = this.startDateAdd;
    this.newExperience.endTime = this.endDateAdd;

    this.profileExperienceService.addExperience(this.newExperience)
      .subscribe(data => {
        this.experiences.push(data);
        console.log("add experience sussecful");
        this.setValidatorAdd();
        this.isAdd = false;
        this.submittedAdd = false;
      });
  }


  edit(experience){
    this.editExperience=experience;
    this.editStartDate = new Date(experience.startTime);
    this.editEndDate = new Date(experience.endTime);
    this.setValidatorEdit(this.editExperience.workplace, this.editExperience.position, this.editExperience.description, new Date(this.editExperience.startTime), new Date(this.editExperience.endTime));
  }

  updateExperience(){
    this.submittedEdit = true;
    var isError = false;

    if(this.editStartDate < new Date(1990,1,1) || this.editStartDate > new Date()){
      this.isErrorStartDayEdit = true;
      isError = true;
    }

    if(this.editEndDate < new Date(1990,1,1) || this.editEndDate > new Date()){
      this.isErrorEndDayEdit = true;
      isError = true;
    }

    if(this.experienceEditForm.invalid){
      isError = true;
    }
    if(this.editEndDate < this.editStartDate){
      this.isErrorDateEdit = true;
      isError = true;
    }
    if(isError){
      return;
    }

    this.editExperience.workplace = this.formEdit.workPlaceEdit.value;
    this.editExperience.position = this.formEdit.positionEdit.value;
    this.editExperience.description = this.formEdit.descriptionEdit.value;
    this.editExperience.startTime = this.editStartDate;
    this.editExperience.endTime = this.editEndDate;

    this.profileExperienceService.updateExperience(this.editExperience)
    .subscribe(data => {
      console.log("edit experience succesfull");
      this.isEdit = false;
    })
  }


  deleteExperience(experience: Experience){
    this.experiences = this.experiences.filter(h => h !== experience);
    this.profileExperienceService.deleteExperience(experience)
      .subscribe(data => {
        console.log("delete experience succesfull");
      })
  }

  onValueChangeStartDateEdit(value: Date){
    this.editStartDate = value;
    this.isErrorDateEdit =  this.submittedEdit && this.editEndDate < this.editStartDate;
    this.isErrorStartDayEdit = this.submittedEdit && ( this.editStartDate < new Date(1990,1,1) || this.editStartDate > new Date());
  }

  onValueChangeEndDateEdit(value: Date){
    this.editEndDate = value;
    this.isErrorDateEdit =  this.submittedEdit && this.editEndDate < this.editStartDate;
    this.isErrorEndDayEdit = this.submittedEdit && ( this.editEndDate < new Date(1990,1,1) ||  this.editEndDate > new Date());
  }

  onValueChangeStartDate(value: Date){
    this.startDateAdd = value;
    this.isErrorDateAdd = this.submittedAdd && this.endDateAdd < this.startDateAdd;
    this.isErrorStartDayAdd = this.submittedAdd && ( this.startDateAdd < new Date(1990,1,1) ||  this.startDateAdd > new Date());

  }

  onValueChangeEndDate(value: Date){
    this.endDateAdd = value;
    this.isErrorDateAdd = this.submittedAdd && this.endDateAdd < this.startDateAdd;
    this.isErrorEndDayAdd = this.submittedAdd && (this.endDateAdd < new Date(1990,1,1) ||  this.endDateAdd > new Date());
  }

  setValidatorAdd(){
    this.experienceAddForm = this.formBuilder.group({
      workPlace : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      position : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      description: ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      fStartDateAdd :  ['', [Validators.required]],
      fEndDateAdd: ['', [Validators.required]],
    }); 
  }

  
  setValidatorEdit(workPlace: string ,position : string, description:string, startDate:Date, endDate:Date){
    this.experienceEditForm = this.formBuilder.group({
      workPlaceEdit : [ workPlace ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      positionEdit : [position ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      descriptionEdit: [description ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
      fStartDateEdit : [startDate, [Validators.required]],
      fEndDateEdit : [endDate, [Validators.required]],
    }); 
  }

}
