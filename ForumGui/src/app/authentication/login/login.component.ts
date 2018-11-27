import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../service/authentication.service';
import { AlertService,  } from '../service/alert.service';
import {CurrentUserInfo} from '../model/current-user-info';
import jsSHA from 'jssha';

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
  error : string;
  isEmailAddress = true;
  patternUsernameOrEmail = '^(([a-zA-Z0-9]{3,15})|([a-zA-Z0-9]{1,}[a-zA-Z0-9.+-]{0,}@[a-zA-Z0-9-]{2,}([.][a-zA-Z]{2,}|[.][a-zA-Z0-9-]{2,}[.][a-zA-Z]{2,}|[.][a-zA-Z0-9-]{2,}[.][a-zA-Z0-9-]{2,}[.][a-zA-Z]{2,})))$';
  currentUserInfo: CurrentUserInfo = new CurrentUserInfo();
  usernameOrEmail: string;
  password: string;
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    ) {}

    ngOnInit() {
      this.loginForm = this.formBuilder.group({
          // username: ['',[Validators.required, Validators.email]],
          username: ['',[Validators.required,Validators.pattern(this.patternUsernameOrEmail)]],
          password: ['',[Validators.required, Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z0-9]).{6,24}$'),Validators.maxLength(24),Validators.minLength(6)]],
          
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
      let shaObj = new jsSHA("SHA-256", "TEXT");
      shaObj.update(this.f.password.value);
      let passwordHash = shaObj.getHash("HEX");
      console.log("hash:"+passwordHash);

      //ma hoa mat khau
      this.authenticationService.login(this.f.username.value, passwordHash)
          .pipe(first())
          .subscribe(            
              data => {
                     if(data != null){
                    //     console.log("co data")
                         this.currentUserInfo = data;
                    //     this.currentUserInfo.roles.forEach( role => {
                    //         {
                    //             if(role === 'Admin')
                    //             {
                    //                this.router.navigate(['/home']);
                    //                this.authenticationService.loggedin();

                    //             }
                    //             else{
                    //                 this.router.navigate(['/register'])
                    //                 this.authenticationService.loggedin();
                    //             }
                    //         }
                    //     });
                        //localStorage.setItem('userInfo', JSON.stringify(this.currentUserInfo));
                        //console.log("userInfo"+JSON.parse(localStorage.getItem('userInfo')));
                        
                    }
                  //this.cookie.set('accessCookie', this.curentUserInfo.token, 0.5);
                  this.router.navigate([this.returnUrl]);
              },
              error => {
                  this.error = this.authenticationService.getErrorLogin(error);
                  this.alertService.error(this.error,true);
                  this.loading = false;
                  
              });
    }

    checkEmailAddress(input: string){
        this.isEmailAddress = input.indexOf('@')>0;
    }

}
