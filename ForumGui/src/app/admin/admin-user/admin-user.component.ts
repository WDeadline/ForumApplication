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

  disable = false;
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
  birthDayAdd : Date;
  birthDayEdit : Date;
  isErrorBirthDayAdd = false;
  isErrorBirthDayEdit = false;
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
      birthDayAdd :  ['', [Validators.required]],
      usernameAdd : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      phoneNumberAdd : ['' ,[Validators.required,Validators.pattern('^([0-9]*)$')]], 
      addressAdd : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
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
        this.usersView = this.users;
        this.usersView.sort((a:User,b:User) => {
          return <any>new Date(b.updationTime) - <any>new Date(a.updationTime);
        })
      })
  }

  addUser(){
    this.submittedAdd = true;

    if(this.birthDayAdd < new Date(1000,1,1) ||this.birthDayAdd > new Date())
    {
      this.isErrorBirthDayAdd = true;
    }

    if(this.userAddForm.invalid){
      return;
    }
 
    this.newUser.name = this.formAdd.nameAdd.value;
    this.newUser.username = this.formAdd.usernameAdd.value;
    this.newUser.emailAddress = this.formAdd.emailAdd.value;
    this.newUser.password = this.formAdd.passwordAdd.value;
    this.newUser.roles.push(this.selectedRoleAdd);
    this.newUser.birthday = this.birthDayAdd;
    this.newUser.address = this.formAdd.addressAdd.value;
    this.newUser.phoneNumber = this.formAdd.phoneNumberAdd.value;

    this.adminUserService.addUser(this.newUser)
      .subscribe(data => {
        this.users.push(data);
        this.usersView = this.users;
        this.usersView.sort((a:User,b:User) => {
          return <any>new Date(b.updationTime) - <any>new Date(a.updationTime);
        })
        this.setValidatorAdd();
        this.submittedAdd = false;
        console.log("add user successful");
        this.isAdd = false;
      },
      error => {
        var errorStr= ""
        errorStr = this.authentication.getErrorRegistor(error);
        this.alertService.error(errorStr, true);
        console.log("error: "+ error);
        this.isAdd = true;
    }
    )

  }

  edit(user){
    this.editUser = user;
    console.log("pass: "+ this.editUser.password)
    this.setValidatorEdit(this.editUser.name,this.editUser.birthday, this.editUser.username,this.editUser.phoneNumber,this.editUser.address, this.editUser.emailAddress, this.editUser.password);
  }

  updateUser(){
    this.submittedEdit = true;

    if(this.birthDayEdit < new Date(1000,1,1) ||this.birthDayEdit > new Date())
    {
      this.isErrorBirthDayEdit = true;
    }

    if(this.userEditForm.invalid){
      return;
    }

    this.editUser.name = this.formEdit.nameEdit.value;
    this.editUser.emailAddress = this.formEdit.emailEdit.value;
    this.editUser.username = this.formEdit.user.value;
    this.editUser.roles.push(this.selectedRoleEdit);
    this.editUser.birthday = this.birthDayEdit;
    this.editUser.address = this.formAdd.addressEdit.value;
    this.editUser.phoneNumber = this.formAdd.phoneNumberEdit.value;

    this.adminUserService.updateUser(this.editUser)
      .subscribe(data => {
        this.isEdit = false;
        console.log("update user successful");
      })

  }

  deleteEducation(user: User){
    this.usersView = this.usersView.filter(h => h !== user);
    this.adminUserService.deleteUser(user)
      .subscribe(data => {console.log("delete user successful")})
  }

  setValidatorAdd(){
    this.userAddForm = this.formBuilder.group({
      nameAdd : [ '' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      birthDayAdd :  ['', [Validators.required]],
      usernameAdd : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      phoneNumberAdd : ['' ,[Validators.required,Validators.pattern('^([0-9]*)$')]], 
      addressAdd : ['' ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      emailAdd: ['' ,[Validators.required,Validators.email]], 
      passwordAdd :  ['', [Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,24}$')]],
    });
  }

  //setValidatorEdit(name: string, username : string, email:string, password:string){
  setValidatorEdit(name: string,birthDay:Date, username : string,phoneNumber:string, address: string, email:string, password:string){
    this.userEditForm = this.formBuilder.group({
      nameEdit : [ name ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      birthDayEdit :  [birthDay, [Validators.required]],
      usernameEdit : [username ,[Validators.required,Validators.pattern('[a-zA-Z0-9]*')]], 
      phoneNumberEdit : [phoneNumber ,[Validators.required,Validators.pattern('^([0-9]*)$')]], 
      addressEdit : [address ,[Validators.required,Validators.pattern('^([^]*[a-zA-Zà-ýÀ-Ýạ-ỹẠ-ỸăĂđĐĩĨũŨơƠưƯ0-9]+[^]*)$')]], 
      emailEdit: [email ,[Validators.required,Validators.email]], 
      passwordEdit :  [password, [Validators.required,Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,24}$')]],
    }); 
  }
  onChange(value) {
    this.selectedRoleAdd = value;
  }

  onChangeEdit(value){
    this.selectedRoleEdit = value;
  }

  onSearchChange(searchValue : string){
    if(searchValue != ''){
      this.usersView = this.users.filter(u => u.name.toLowerCase().indexOf(searchValue.toLowerCase())> -1);
    }else{
      this.usersView = this.users;
    }
    console.log(searchValue);
  }

  onValueChangeAdd(value: Date){
    this.birthDayAdd = value;
    this.isErrorBirthDayAdd = this.submittedAdd && (this.birthDayAdd < new Date(1000,1,1) ||this.birthDayAdd > new Date());
  }

  onValueChangeEdit(value:Date){
    this.birthDayEdit = value;
    this.isErrorBirthDayEdit = this.submittedEdit && (this.birthDayEdit < new Date(1000,1,1) ||this.birthDayEdit > new Date());
  }

}
