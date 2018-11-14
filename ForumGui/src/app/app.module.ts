import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './share/header/header.component';
import { FooterComponent } from './share/footer/footer.component';
import { LeftMenuComponent } from './share/left-menu/left-menu.component';
import { LoginComponent } from './authentication/login/login.component';
import { HomeContentComponent } from './home-content/home-content.component';
import { RegisterComponent } from './authentication/register/register.component';
import { AllUserComponent } from './user/all-user/all-user.component';
import { ProfileComponent } from './user/profile/profile.component';
import { ProfileEditComponent } from './user/profile-edit/profile-edit.component';

import { ConfigService } from './discussion/config.service';
import { InputFormatComponent } from './discussion/input-format/input-format.component';

import { HttpClientModule,HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NgxEditorModule } from 'ngx-editor';
import { QuestionMakeComponent } from './discussion/question-make/question-make.component';
import { QuestionShowComponent } from './discussion/question-show/question-show.component';

import { ReactiveFormsModule }    from '@angular/forms';
import { ErrorInterceptor } from './authentication/helper/error.interceptor';
import { JwtInterceptor } from './authentication/helper/jwt.interceptor';
import { TestComponent } from './test/test.component';
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
    ProfileEditComponent,
    InputFormatComponent,
    QuestionMakeComponent,
    QuestionShowComponent,
    TestComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgxEditorModule,
    ReactiveFormsModule
  ],
  providers: [
    ConfigService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
