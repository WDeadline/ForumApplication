import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }    from '@angular/forms';

import {ProfileUpdateComponent} from './profile-update/profile-update.component';
import { ProfileUpdateRoutingModule } from './profile-update-routing.module';
import { ProfileInfomationComponent } from './profile-infomation/profile-infomation.component';
import { ProfilePositionComponent } from './profile-position/profile-position.component';
import { ProfileObjectiveComponent } from './profile-objective/profile-objective.component';
import { ProfileExperienceComponent } from './profile-experience/profile-experience.component';
import {ProfileEducationComponent} from './profile-education/profile-education.component';
import { ProfileSkillComponent } from './profile-skill/profile-skill.component';

@NgModule({
  declarations: [
    ProfileUpdateComponent,
    ProfileInfomationComponent,
    ProfilePositionComponent,
    ProfileObjectiveComponent,
    ProfileExperienceComponent,
    ProfileEducationComponent,
    ProfileSkillComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ProfileUpdateRoutingModule
  ]
})
export class ProfileUpdateModule { }
