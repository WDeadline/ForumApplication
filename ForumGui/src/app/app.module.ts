import { BrowserModule } from '@angular/platform-browser';
import { NgModule,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { LeftMenuComponent } from './left-menu/left-menu.component';
import { LoginComponent } from './authentication/login/login.component';
import { HomeContentComponent } from './home-content/home-content.component';
import { RegisterComponent } from './authentication/register/register.component';
import { AllUserComponent } from './user/all-user/all-user.component';
import { ProfileComponent } from './user/profile/profile.component';

import { InputFormatComponent } from './discussion/input-format/input-format.component';

import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {EditorModule} from 'primeng/primeng';
import { QuestionMakeComponent } from './discussion/question-make/question-make.component';
import { QuestionShowComponent } from './discussion/question-show/question-show.component';

import { ReactiveFormsModule }    from '@angular/forms';
import { ErrorInterceptor } from './authentication/helper/error.interceptor';
import { JwtInterceptor } from './authentication/helper/jwt.interceptor';
import { TestComponent } from './test/test.component';
import { CookieService } from 'ngx-cookie-service';

import {AlertService} from './authentication/service/alert.service';
import { from } from 'rxjs';
import {AlertComponent} from './authentication/directives/alert.component';


import { TagInputModule } from 'ngx-chips';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // this is needed!


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LeftMenuComponent,
    LoginComponent,
    HomeContentComponent,
    RegisterComponent,
    AllUserComponent,
    ProfileComponent,
    InputFormatComponent,
    QuestionMakeComponent,
    QuestionShowComponent,
    TestComponent,
    AlertComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    EditorModule,
    TagInputModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
  ],
  providers: [
    CookieService,
    AlertService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
})
export class AppModule { }
