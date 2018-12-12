import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../service/authentication.service';
import jsSHA from 'jssha';
import {AlertService} from '../service/alert.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  same: boolean;
  
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
  ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({

      //firstName : ['', [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
      lastName : ['',  [Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]],
      username : ['', [Validators.required,Validators.pattern('[a-zA-Z0-9]*')]],
      emailAddress : ['',[Validators.required, Validators.email]],
      password : ['', [Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,24}$'),Validators.maxLength(24),Validators.minLength(6)]],
      passwordConfirm : ['',[Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,24}$'),Validators.maxLength(24),Validators.minLength(6)]],
      
  });
  }

  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.registerForm.invalid) {
        return;
    }
    console.log("out")
    this.checkPasswords();
    this.loading = true;
    

    //this.f.firstName.value, this.f.lastName.value,
    this.authenticationService.register(this.f.lastName.value,
      this.f.username.value,this.f.emailAddress.value, this.f.password.value)
        .pipe(first())
          .subscribe(
            data => {
              //this.cookieService.set('accessCookie', this.currentUserInfo.accessToken, 0.5);
              this.loading = false;             
              this.router.navigate(['/login']);
              // chua thong bao success duoc
              //this.alertService.success("Registration successful", true);
                           
          },
          error => {
              this.error = this.authenticationService.getErrorRegistor(error);
              this.alertService.error(this.error, true);
              console.log("error: "+ error);
              this.loading = false;
          });
  }

  checkPasswords(){
    console.log("checkPasswords start ");
    let password = this.f.password.value;
    let confirmPassword = this.f.passwordConfirm.value; 
    password === confirmPassword ? this.same = true : this.same = false;   
    if(!this.same){
      console.log("not same: " + this.same);
      return;
    }
  }


}
