import { Component, OnInit } from '@angular/core';
import {ProfileActivityService} from '../user/profile-activity/profile-activity.service';
import {ProfileEducationService} from '../user/profile-education/profile-education.service';
import {ProfileExperienceService} from '../user/profile-experience/profile-experience.service';
import {ProfileInformationService} from '../user/profile-infomation/profile-information.service';
import {ProfileObjectiveService} from '../user/profile-objective/profile-objective.service';
import {ProfileSkillService} from '../user/profile-skill/profile-skill.service';
import {User} from '../user/model1/user';
import {Activity} from '../user/model1/activity';
import {Education} from '../user/model1/education';
import {Experience} from '../user/model1/experience';
import {Objective} from '../user/model1/objective';
import {Skill} from '../user/model1/skill';
import { Subscription } from 'rxjs';
import { ActivatedRoute,Router } from '@angular/router';
@Component({
  selector: 'app-profile-detail',
  templateUrl: './profile-detail.component.html',
  styleUrls: ['./profile-detail.component.css']
})
export class ProfileDetailComponent implements OnInit {

  information : User[] = [];
  educations : Education[];
  objectives : Objective[];
  experiences : Experience[] = [];
  activities : Activity[];
  skills : Skill[];
  subscription : Subscription;
  id: string='';
  imageURL: string = "assets/img/default.png";
  constructor(
    private profileActivityService :ProfileActivityService,
    private profileEducationService : ProfileEducationService,
    private profileExperienceService : ProfileExperienceService,
    private profileInformationService : ProfileInformationService,
    private profileObjectiveService: ProfileObjectiveService,
    private profileSkillService : ProfileSkillService,
    private router : Router,
    private activateRoute: ActivatedRoute,

  ) { }

  ngOnInit() {
    this.subscription = this.activateRoute.queryParams.subscribe(val=>{
      if (val['id'])
        this.id = val['id'];
        if(this.id == null)
        {
          this.router.navigate(['/home']);
        } 
    });
    this.getInformation();
    this.getEducations();
    this.getObjectives();
    this.getExperiences();
    this.getActivity();
    this.getSkills();
  }

  getInformation(){
    this.profileInformationService.getInformationById(this.id)
      .subscribe(data => {
        this.information.push(data);
      });
  }

  getEducations(){
    this.profileEducationService.getEducationsById(this.id)
      .subscribe(data => this.educations = data);
  }

  getObjectives(){
    this.profileObjectiveService.getObjectivesById(this.id)
      .subscribe(data => this.objectives = data);
  }

  getExperiences(){
    this.profileExperienceService.getExperiencesById(this.id)
      .subscribe(data => this.experiences = data);
  }

  getActivity(){
    this.profileActivityService.getActivitiesById(this.id)
      .subscribe(data => this.activities = data)
  }

  getSkills(){
    this.profileSkillService.getSkillsById(this.id)
      .subscribe(data => this.skills = data);
}

  

}
