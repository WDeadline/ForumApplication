import { Component, OnInit } from '@angular/core';
import {ProfileSkillService} from './profile-skill.service';
import {Skill} from '../model/skill';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';

@Component({
  selector: 'app-profile-skill',
  templateUrl: './profile-skill.component.html',
  styleUrls: ['./profile-skill.component.css']
})
export class ProfileSkillComponent implements OnInit {

  
  skillFormAdd: FormGroup;
  skillFormEdit: FormGroup;
  isEdit = false;
  isAdd =false;
  submittedAdd = false;
  submittedEdit = false;
  max = 5;
  rate = 3;
  isReadonly = false;
 
  overStar: number;
  percent: number;
  
  constructor(
    private profileSkillService : ProfileSkillService,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
  ) { }

  ngOnInit() {
    this.skillFormAdd = this.formBuilder.group({
      skillAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
      ratingAdd : [1, Validators.required],      
    });
  }

  get formAdd() { return this.skillFormAdd.controls; }
  
  get formEdit(){return this.skillFormEdit.controls; }

  edit(){
    this.editForm();
  }

  updateSkill(){

  }

  deleteSkill(){

  }

  hoveringOver(value: number): void {
    this.overStar = value;
    this.percent = (value / this.max) * 100;
  }
 
  resetStar(): void {
    this.overStar = void 0;
  }

  addSkill(){
    console.log("skill"+ this.rate);
  }

  //editForm(skillName: string){
  editForm(){
    this.skillFormEdit = this.formBuilder.group({
      skillEdit : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
      ratingEdit : [1, Validators.required], 
    }); 
  }

}
