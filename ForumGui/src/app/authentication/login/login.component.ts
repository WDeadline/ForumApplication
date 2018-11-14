import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../service/authentication.service';

//import { Cookie } from 'ng2-cookies';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';
  isEmailAddress = true;
  patternUsernameOrEmail = '^(([a-zA-Z0-9]{3,15})|([a-zA-Z0-9]{1,}[a-zA-Z0-9.+-]{0,}@[a-zA-Z0-9-]{2,}([.][a-zA-Z]{2,}|[.][a-zA-Z0-9-]{2,}[.][a-zA-Z]{2,}|[.][a-zA-Z0-9-]{2,}[.][a-zA-Z0-9-]{2,}[.][a-zA-Z]{2,})))$';

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

      // reset login status
      this.authenticationService.logout();

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
                  //Cookie.set('accessCookie', this.userInfo.accessToken, 0.5);
                  this.router.navigate([this.returnUrl]);
              },
              error => {
                  console.log("error: "+ error);
                  if(error == 'Bad Request'){
                    this.error = "Your username and/or password do not match";
                  }
                  else{
                    this.error = error;
                    this.error
                  }
                  this.loading = false;
              });
    }

    checkEmailAddress(input: string){
        this.isEmailAddress = input.indexOf('@')>0;
    }

}
