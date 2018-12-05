import { Component, OnInit } from '@angular/core';
//import {ProfileSkillService} from './profile-skill.service';
//import {Skill} from '../model/skill';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';

@Component({
  selector: 'app-profile-activity',
  templateUrl: './profile-activity.component.html',
  styleUrls: ['./profile-activity.component.css']
})
export class ProfileActivityComponent implements OnInit {

  activityFormAdd : FormGroup;
  activityFormEdit: FormGroup;
  submittedAdd = false;
  submittedEdit = false;
  constructor(
    //private profileSkillService : ProfileSkillService,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
  ) { }

  ngOnInit() {
    this.activityFormAdd = this.formBuilder.group({
      activityNameAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],          
    });
  }

  get formAdd() { return this.activityFormAdd.controls; }

  get formEdit() { return this.activityFormEdit.controls; }



  addActivity(){

  }

  edit(){
    this.editForm();
  }


  updateActivity(){

  }

  deleteActivity(){

  }

  /* Event change datetimePickerAdd*/
  onValueChangeAdd(value: Date){

  }

  /* Event change datetimePickerEdit*/
  onValueChangeEdit(value: Date){

  }

  //editForm(activityName: string){
  editForm(){
    this.activityFormEdit = this.formBuilder.group({
      activityNameEdit : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
    }); 
  }
}
