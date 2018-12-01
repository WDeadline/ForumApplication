import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AdminUserComponent} from './admin-user/admin-user.component';
import {AdminTagComponent} from './admin-tag/admin-tag.component';
import {AdminQuestionComponent} from './admin-question/admin-question.component';
import {AdminVoteComponent} from './admin-vote/admin-vote.component';
import {AdminViewComponent} from './admin-view/admin-view.component';
import {AdminReportComponent} from './admin-report/admin-report.component';
import {AdminAnwerComponent} from './admin-anwer/admin-anwer.component';
import {AdminCommentComponent} from './admin-comment/admin-comment.component';
import {AdminWorkComponent} from './admin-work/admin-work.component';
import {AdminRecruitmentComponent} from './admin-recruitment/admin-recruitment.component';
import {AdminInterviewComponent} from './admin-interview/admin-interview.component';

const routes: Routes = [
  { 
    path: 'user', component: AdminUserComponent 
  },
  { 
    path: 'tag', component: AdminTagComponent 
  },
  { 
    path: 'question', component: AdminQuestionComponent 
  },
  { 
    path: 'vote', component: AdminVoteComponent 
  },
  { 
    path: 'view', component: AdminViewComponent 
  },
  { 
    path: 'report', component: AdminReportComponent 
  },
  { 
    path: 'anwer', component: AdminAnwerComponent 
  },
  { 
    path: 'comment', component: AdminCommentComponent 
  },
  { 
    path: 'work', component: AdminWorkComponent 
  },
  { 
    path: 'recruitment', component: AdminRecruitmentComponent 
  },
  { 
    path: 'interview', component: AdminInterviewComponent 
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
