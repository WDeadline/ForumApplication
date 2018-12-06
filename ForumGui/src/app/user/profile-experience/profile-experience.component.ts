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
  startDate : Date;
  endDate: Date;
  experiences : Experience[] = [];
  editStartDate : Date;
  editEndDate : Date;
  valueChangeEditStartDate :Date;
  valueChangeEditEndDate: Date;
  errEdit : string ;
  errAdd: string;
  constructor(
    private authenticationService : AuthenticationService,
    private formBuilder: FormBuilder,
    private profileExperienceService: ProfileExperienceService,
  ) { }

  ngOnInit() {
    this.getExperiences();
    this.experienceAddForm = this.formBuilder.group({
      workPlace : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      position : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      description: ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
    }); 

  }

  get formAdd() { return this.experienceAddForm.controls; }

  get formEdit() { return this.experienceEditForm.controls; }

  getExperiences(){
    this.profileExperienceService.getExperiences()
      .subscribe(data => this.experiences = data);
  }

  addExperience(){

    this.submittedAdd = true;

    if (this.experienceAddForm.invalid) {
      return;
    }

    if(this.endDate <= this.startDate){
      this.errAdd ="The end date must be greater than the start date";
      return;
    }

    this.newExperience.workplace =  this.formAdd.workPlace.value;
    this.newExperience.position = this.formAdd.position.value;
    this.newExperience.description =  this.formAdd.description.value;
    if(this.startDate){
      this.newExperience.startTime = this.startDate;
    }
    if(this.endDate){
      this.newExperience.endTime = this.endDate;
    }
    this.profileExperienceService.addExperience(this.newExperience)
      .subscribe(data => {
        this.experiences.push(data);
        //this.formAdd.workPlace.
        this.isAdd = false;
      });
  }

  setValidator(str1: string ,str2 : string, str3:string){
    this.experienceEditForm = this.formBuilder.group({
      workPlaceEdit : [ str1 ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      positionEdit : [str2 ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      descriptionEdit: [str3 ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
    }); 
  }

  edit(experience){
    this.editExperience=experience;
    this.setValidator(this.editExperience.workplace, this.editExperience.position, this.editExperience.description);
    this.editStartDate = new Date(this.editExperience.startTime);
    this.editEndDate = new Date (this.editExperience.endTime);
  }

  updateExperience(){
    this.submittedEdit = true;
    if(this.experienceEditForm.invalid){
      return;
    }
    if(this.valueChangeEditEndDate <= this.valueChangeEditStartDate){
      this.errEdit ="The end date must be greater than the start date";
      return;
    }

    this.editExperience.workplace = this.formEdit.workPlaceEdit.value;
    this.editExperience.position = this.formEdit.positionEdit.value;
    this.editExperience.description = this.formEdit.descriptionEdit.value;
    if(this.valueChangeEditStartDate){
      this.editExperience.startTime = this.valueChangeEditStartDate;
    }
    if(this.valueChangeEditEndDate){
      this.editExperience.endTime = this.valueChangeEditEndDate;
    }
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
    this.valueChangeEditStartDate = value;
  }

  onValueChangeEndDateEdit(value: Date){
    this.valueChangeEditEndDate = value;
  }

  onValueChangeStartDate(value: Date){
    this.startDate = value;
  }

  onValueChangeEndDate(value: Date){
    this.endDate = value;
  }

}
