import { Component, OnInit } from '@angular/core';
import {AdminUserService} from './admin-user.service';
import {User} from '../../user/model1/user';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import {AlertService} from '../../authentication/service/alert.service';

import jsSHA from 'jssha';
@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css']
})
export class AdminUserComponent implements OnInit {

  users : User[];
  usersView : User[];
  stt : number = 0;
  isAdd = false;
  isEdit =false;
  userAddForm: FormGroup;
  userEditForm: FormGroup;
  submittedAdd = false;
  submittedEdit = false;
  newUser :User = new User();
  editUser : User = new User();
  selectedRoleAdd: number = 0;
  selectedRoleEdit: number = 0;
  loading = false;
  constructor(
    private adminUserService:AdminUserService,
    private formBuilder: FormBuilder,
    private authentication : AuthenticationService,
    private alertService :AlertService,
  ) { }

  ngOnInit() {
    this.getAllUsers();
    this.userAddForm = this.formBuilder.group({
      nameAdd : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      usernameAdd : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      emailAdd: ['' ,[Validators.required,Validators.email]], 
      passwordAdd :  ['', [Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,24}$')]],
    });
  }

  get formAdd() { return this.userAddForm.controls; }

  get formEdit() { return this.userEditForm.controls; }
  
  getAllUsers(){
    this.adminUserService.getAllUser()
      .subscribe(data => {
        this.users = data;
        this.usersView = data;
        this.usersView.sort((a:User,b:User) => {
          return <any>new Date(b.updationTime) - <any>new Date(a.updationTime);
        })
      })
  }

  addUser(){
    this.submittedAdd = true;
    if(this.userAddForm.invalid){
      return;
    }
    this.loading = true;
    let shaObj = new jsSHA("SHA-256", "TEXT");
    shaObj.update(this.formAdd.passwordAdd.value);
    let passwordHash = shaObj.getHash("HEX");


    this.newUser.name = this.formAdd.nameAdd.value;
    this.newUser.username = this.formAdd.usernameAdd.value;
    this.newUser.emailAddress = this.formAdd.emailAdd.value;
    this.newUser.password = passwordHash;
    this.newUser.roles.push(this.selectedRoleAdd);

    this.adminUserService.addUser(this.newUser)
      .subscribe(data => {
        this.users.push(data);
        this.usersView.push(data);
        this.usersView.sort((a:User,b:User) => {
          return <any>new Date(b.updationTime) - <any>new Date(a.updationTime);
        })
        console.log("add user successful");
        this.isAdd = false;
      },
      error => {
        var errorStr= ""
        errorStr = this.authentication.getErrorRegistor(error);
        this.alertService.error(errorStr, true);
        console.log("error: "+ error);
        this.loading = false;
        this.isAdd = true;
    }
    )

  }

  edit(){
    this.setValidatorEdit();
  }

  deleteEducation(user: User){
    this.users = this.users.filter(h => h !== user);
    this.adminUserService.deleteUser(user)
      .subscribe(data => {console.log("delete user successful")})
  }

  setValidatorAdd(){
    this.userAddForm = this.formBuilder.group({
      nameAdd : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      usernameAdd : ['' ,[Validators.required,Validators.pattern('[a-zA-Z0-9]*')]], 
      emailAdd: ['' ,[Validators.required,Validators.email]], 
      passwordAdd :  ['', [Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,24}$')]],
    }); 
  }

  //setValidatorEdit(name: string, username : string, email:string, password:string){
  setValidatorEdit(){
    this.userEditForm = this.formBuilder.group({
      nameEdit : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      usernameEdit : ['' ,[Validators.required,Validators.pattern('[a-zA-Z0-9]*')]], 
      emailEdit: ['' ,[Validators.required,Validators.email]], 
      passwordEdit :  ['', [Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,24}$')]],
    }); 
  }
  onChange(deviceValue) {
    this.selectedRoleAdd = deviceValue;
  }

  onSearchChange(searchValue : string){
    if(searchValue != ''){
      this.usersView = this.users.filter(u => u.name.toLowerCase().indexOf(searchValue.toLowerCase())> -1);
    }else{
      this.usersView = this.users;
    }
    console.log(searchValue);
  }

}
