import { Component, OnInit } from '@angular/core';
import {ProfileActivityService} from './profile-activity.service';
import {Activity} from '../model1/activity';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';

@Component({
  selector: 'app-profile-activity',
  templateUrl: './profile-activity.component.html',
  styleUrls: ['./profile-activity.component.css']
})
export class ProfileActivityComponent implements OnInit {

  isAdd = false;
  isEdit = false;
  activityFormAdd : FormGroup;
  activityFormEdit: FormGroup;
  submittedAdd = false;
  submittedEdit = false;
  newActivity : Activity = new Activity();
  editActivity : Activity = new Activity();
  activities : Activity[];
  joinDateAdd : Date;
  joinDateEdit : Date;

  isErrorJoinDayAdd : boolean = false;
  isErrorJoinDayEdit : boolean = false;
  constructor(
    private profileActivityService : ProfileActivityService,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,

  ) { }

  ngOnInit() {
    this.getActivity();
    this.activityFormAdd = this.formBuilder.group({
      activityNameAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],          
      joinDateAdd :  ['', [Validators.required]],
    });
  }

  get formAdd() { return this.activityFormAdd.controls; }

  get formEdit() { return this.activityFormEdit.controls; }

  getActivity(){
    this.profileActivityService.getActivities()
      .subscribe(data => this.activities = data)
  }

  addActivity(){
    this.submittedAdd = true;
    
    if(this.joinDateAdd < new Date(1990,1,1) || this.joinDateAdd > new Date()){
      this.isErrorJoinDayAdd = true;
      return;
    }
    if (this.activityFormAdd.invalid) {
      return;
    }
    
    this.newActivity.name = this.formAdd.activityNameAdd.value;
    this.newActivity.joinDate = this.joinDateAdd;
    this.profileActivityService.addActivity( this.newActivity)
      .subscribe(data => {
        this.activities.push(data);
        this.activityFormAdd = this.formBuilder.group({
          activityNameAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],          
        });
        this.isAdd = false;
        this.submittedAdd = false;
      })
  }

  edit(activity){
    this.editActivity = activity;
    this.editForm(this.editActivity.name,new Date( this.editActivity.joinDate));
    this.joinDateEdit = new Date(this.editActivity.joinDate);
  }


  updateActivity(){
    this.submittedEdit = true;
    if(this.joinDateAdd < new Date(1990,1,1) || this.joinDateAdd > new Date()){
      this.isErrorJoinDayAdd = true;
      return;
    }

    if(this.activityFormEdit.invalid){
      return;
    }
    this.editActivity.name = this.formEdit.activityNameEdit.value;
    if(this.joinDateEdit){
      this.editActivity.joinDate = this.joinDateEdit;
    }
    this.profileActivityService.updateActivity(this.editActivity)
      .subscribe(data => {
        console.log("edit activity succesfull");
        this.isEdit = false;
      })

  }

  deleteActivity(activity){
    this.activities = this.activities.filter(h => h !== activity);
    this.profileActivityService.deleteActivity(activity)
      .subscribe(data =>{
        console.log("delete activity succesfull");
      })
  }

  /* Event change datetimePickerAdd*/
  onValueChangeAdd(value: Date){
    this.joinDateAdd = value;
    this.isErrorJoinDayAdd = this.submittedAdd && (this.joinDateAdd < new Date(1990,1,1) || this.joinDateAdd > new Date());
  }

  /* Event change datetimePickerEdit*/
  onValueChangeEdit(value: Date){
    this.joinDateEdit = value;
    this.isErrorJoinDayEdit = this.submittedEdit && (this.joinDateEdit < new Date(1990,1,1) || this.joinDateEdit > new Date());
  }

  editForm(activityName: string, joinDate: Date){
    this.activityFormEdit = this.formBuilder.group({
      activityNameEdit : [activityName, [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
      joinDateEdit :  [joinDate, [Validators.required]],
    }); 
  }
}
