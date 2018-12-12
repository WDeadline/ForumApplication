import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './authentication/login/login.component';
import {HomeContentComponent} from './home-content/home-content.component';
import {RegisterComponent} from './authentication/register/register.component';
import {AllUserComponent} from './user/all-user/all-user.component';
import { AuthGuard } from './authentication/guard/auth.guard';
import {TestComponent} from './test/test.component';
import {QuestionMakeComponent} from './discussion/question-make/question-make.component';
import {ProfileComponent} from './user/profile/profile.component';
import {QuestionShowComponent} from './discussion/question-show/question-show.component';
import {TagComponent} from './tag/tag.component';
import {JobComponent} from './job/job.component';
import {JobDetailComponent} from './job-detail/job-detail.component';
import {RecruitmentComponent} from './recruitment/recruitment.component';
import {RecruitmentDetailComponent} from './recruitment-detail/recruitment-detail.component';
import {ManageJobComponent} from './company/manage-job/manage-job.component';
import {ManageRecruitmentComponent} from './company/manage-recruitment/manage-recruitment.component';
import {ProfileDetailComponent} from './profile-detail/profile-detail.component';
const routes: Routes = [
  { path: 'home', component: HomeContentComponent },
  { path: 'test', component: TestComponent ,canActivate: [AuthGuard]},
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: AllUserComponent},
  { path: 'tags', component: TagComponent},
  { path: 'jobs', component: JobComponent},
  { path: 'register', component: RegisterComponent },
  { path: 'makequestion', component: QuestionMakeComponent,canActivate: [AuthGuard] },
  { path: 'showquestion', component: QuestionShowComponent},
  { path: 'profile', component: ProfileComponent,canActivate: [AuthGuard]},
  { path: 'jobdetail', component: JobDetailComponent },
  { path: 'manage-job', component: ManageJobComponent,canActivate: [AuthGuard] },
  { path: 'manage-recruitment', component: ManageRecruitmentComponent,canActivate: [AuthGuard] }, 
  { path: 'showCV', component: ProfileDetailComponent,canActivate: [AuthGuard] }, 
  {
    path: 'profile-update',
    loadChildren: './user/profile-update.module#ProfileUpdateModule',
    canActivate: [AuthGuard]
  },
  {
    path: 'admin',
    loadChildren: './admin/admin.module#AdminModule',
    canActivate: [AuthGuard]
  },
  {
    path: 'recruitment',
    component : RecruitmentComponent,
    canActivate: [AuthGuard]
  },

  {
    path: 'recruitmentdetail',
    component : RecruitmentDetailComponent,
    canActivate: [AuthGuard]
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
