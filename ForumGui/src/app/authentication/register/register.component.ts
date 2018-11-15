import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../service/authentication.service';

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
  ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({

      firstName : ['', Validators.required],
      lastName : ['', Validators.required],
      username : ['', Validators.required],
      emailAddress : ['',[Validators.required, Validators.email]],
      password : ['', Validators.required],
      passwordConfirm : ['', Validators.required],
      
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
    //ma hoa mat khau
    this.authenticationService.register(this.f.firstName.value, this.f.lastName.value,
      this.f.username.value,this.f.emailAddress.value, this.f.password.value)
        .pipe(first())
          .subscribe(
            data => {
              //this.cookieService.set('accessCookie', this.currentUserInfo.accessToken, 0.5);
              this.router.navigate(['/home']);
              this.loading = false;
          },
          error => {
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
