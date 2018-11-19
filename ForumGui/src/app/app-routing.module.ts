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

const routes: Routes = [
  { path: 'home', component: HomeContentComponent ,canActivate: [AuthGuard]},
  { path: 'test', component: TestComponent ,canActivate: [AuthGuard]},
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'users', component: AllUserComponent},
  { path: 'register', component: RegisterComponent },
  { path: 'makequestion', component: QuestionMakeComponent,canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent},
  {
    path: 'profile-update',
    loadChildren: './user/profile-update.module#ProfileUpdateModule'
  },
  

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
