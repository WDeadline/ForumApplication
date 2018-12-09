import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule }    from '@angular/forms';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminUserComponent } from './admin-user/admin-user.component';
import { AdminTagComponent } from './admin-tag/admin-tag.component';
import { AdminQuestionComponent } from './admin-question/admin-question.component';
import { AdminVoteComponent } from './admin-vote/admin-vote.component';
import { AdminViewComponent } from './admin-view/admin-view.component';
import { AdminReportComponent } from './admin-report/admin-report.component';
import { AdminAnwerComponent } from './admin-anwer/admin-anwer.component';
import { AdminCommentComponent } from './admin-comment/admin-comment.component';
import { AdminWorkComponent } from './admin-work/admin-work.component';
import { AdminRecruitmentComponent } from './admin-recruitment/admin-recruitment.component';
import { AdminInterviewComponent } from './admin-interview/admin-interview.component';


@NgModule({
  declarations: [AdminUserComponent, AdminTagComponent, AdminQuestionComponent, AdminVoteComponent, AdminViewComponent, AdminReportComponent, AdminAnwerComponent, AdminCommentComponent, AdminWorkComponent, AdminRecruitmentComponent, AdminInterviewComponent ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule, //su dung cho [formGroup] in html
    AdminRoutingModule
  ]
})
export class AdminModule { }
