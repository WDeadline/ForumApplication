import { Component, OnInit } from '@angular/core';
import {ProfileSkillService} from './profile-skill.service';
import {Skill} from '../model1/skill';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';

@Component({
  selector: 'app-profile-skill',
  templateUrl: './profile-skill.component.html',
  styleUrls: ['./profile-skill.component.css']
})
export class ProfileSkillComponent implements OnInit {

  rateAdd = 1;
  rateEdit = 0;
  skillFormAdd: FormGroup;
  skillFormEdit: FormGroup;
  isEdit = false;
  isAdd =false;
  submittedAdd = false;
  submittedEdit = false;
  newSkill : Skill = new Skill();
  editSkill : Skill = new Skill();
  skills : Skill[];
  max = 5;
  rate = 3;
  isReadonly = false;
 
  overStar: number;
  percent: number;
  
  constructor(
    private profileSkillService : ProfileSkillService,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.getSkills();
    this.skillFormAdd = this.formBuilder.group({
      skillAdd : ['', [Validators.required, Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
      ratingAdd : [1, Validators.required],      
    });
  }

  get formAdd() { return this.skillFormAdd.controls; }
  
  get formEdit(){return this.skillFormEdit.controls; }
   
  getSkills(){
      this.profileSkillService.getSkills()
        .subscribe(data => this.skills = data);
  }

  addSkill(){
    this.submittedAdd = true;
    if (this.skillFormAdd.invalid) {
      return;
    }
    this.newSkill.name = this.formAdd.skillAdd.value;
    this.newSkill.level = this.rateAdd;
    console.log("skill"+ this.rateAdd);
    this.profileSkillService.addSkill(this.newSkill)
      .subscribe(data => {
        console.log("add Objective sussectfule");
        this.skills.push(data);
        this.skillFormAdd = this.formBuilder.group({
          skillAdd : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
          ratingAdd : [1, Validators.required],     
        });
        this.rateAdd =0 ;
        this.isAdd = false;
        this.submittedAdd = false;
      });   
  }

  edit(skill){
    this.editSkill = skill;
    this.setValidation(this.editSkill.name);
    this.rateEdit = this.editSkill.level;
  }

  updateSkill(){
    this.submittedEdit = true;
    if (this.skillFormEdit.invalid) {
      return;
    }
      this.editSkill.name = this.formEdit.skillEdit.value;
      this.editSkill.level = this.rateEdit;
      this.profileSkillService.updateSkill(this.editSkill)
      .subscribe(data => {
        console.log("edit skill succesfull");
        this.isEdit = false;
      });

  }

  deleteSkill(skill: Skill){
    this.skills = this.skills.filter(h => h !== skill );
    this.profileSkillService.deleteSkill(skill)
      .subscribe(data =>{
        console.log("delete skill succesful");
      })

  }

  setValidation(skillName : string){
    this.skillFormEdit = this.formBuilder.group({
      skillEdit : [skillName, [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],      
      ratingEdit : [1, Validators.required], 
    }); 
  }

}
