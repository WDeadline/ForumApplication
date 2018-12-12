import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule }    from '@angular/forms';

import {ProfileUpdateComponent} from './profile-update/profile-update.component';
import { ProfileUpdateRoutingModule } from './profile-update-routing.module';
import { ProfileInfomationComponent } from './profile-infomation/profile-infomation.component';
import { ProfilePositionComponent } from './profile-position/profile-position.component';
import { ProfileObjectiveComponent } from './profile-objective/profile-objective.component';
import { ProfileExperienceComponent } from './profile-experience/profile-experience.component';
import {ProfileEducationComponent} from './profile-education/profile-education.component';
import { ProfileSkillComponent } from './profile-skill/profile-skill.component';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';

import { RatingModule } from 'ngx-bootstrap/rating';
import { ProfileActivityComponent } from './profile-activity/profile-activity.component';


@NgModule({
  declarations: [
    ProfileUpdateComponent,
    ProfileInfomationComponent,
    ProfilePositionComponent,
    ProfileObjectiveComponent,
    ProfileExperienceComponent,
    ProfileEducationComponent,
    ProfileSkillComponent,
    ProfileActivityComponent,

  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule, //su dung cho [formGroup] in html
    BsDatepickerModule.forRoot(), 
    RatingModule.forRoot(), //ng-bootstrap rating
    ProfileUpdateRoutingModule
  ]
})
export class ProfileUpdateModule { }
