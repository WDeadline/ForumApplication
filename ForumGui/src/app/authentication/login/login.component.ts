import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../service/authentication.service';
import {CurrentUserInfo} from '../model/current-user-info';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error : [];
  isEmailAddress = true;
  patternUsernameOrEmail = '^(([a-zA-Z0-9]{3,15})|([a-zA-Z0-9]{1,}[a-zA-Z0-9.+-]{0,}@[a-zA-Z0-9-]{2,}([.][a-zA-Z]{2,}|[.][a-zA-Z0-9-]{2,}[.][a-zA-Z]{2,}|[.][a-zA-Z0-9-]{2,}[.][a-zA-Z0-9-]{2,}[.][a-zA-Z]{2,})))$';
  currentUserInfo: CurrentUserInfo = new CurrentUserInfo();
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    ) {}

    ngOnInit() {
      this.loginForm = this.formBuilder.group({
          // username: ['',[Validators.required, Validators.email]],
          username: ['',[Validators.required,Validators.pattern(this.patternUsernameOrEmail)]],
          password: ['',[Validators.required, Validators.pattern('^([a-zA-Z0-9]+[^]{3,15})$')]],
          
      });

      // get return url from route parameters or default to '/'
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
      
    }
    // convenience getter for easy access to form fields
    get f() { return this.loginForm.controls; }

    onSubmit() {
      this.submitted = true;
      this.checkEmailAddress(this.f.username.value);
      // stop here if form is invalid
      if (this.loginForm.invalid) {
          return;
      }
      
      this.loading = true;
      //ma hoa mat khau
      this.authenticationService.login(this.f.username.value, this.f.password.value)
          .pipe(first())
          .subscribe(            
              data => {
                    if(data != null){
                        this.currentUserInfo = data;
                        this.currentUserInfo.roles.forEach( role => {
                            {
                                if(role === 'Admin')
                                {
                                   this.router.navigate(['/home']);

                                }
                                else{
                                    this.router.navigate(['/register'])
                                }
                            }
                        });
                        localStorage.setItem('userRole', this.currentUserInfo.roles[0]);
                    }
                  //this.cookie.set('accessCookie', this.curentUserInfo.token, 0.5);
                  //this.router.navigate([this.returnUrl]);
              },
              error => {
                  this.error = this.authenticationService.getErrorLogin(error);
                  this.loading = false;
                  
              });
    }

    checkEmailAddress(input: string){
        this.isEmailAddress = input.indexOf('@')>0;
    }

}
