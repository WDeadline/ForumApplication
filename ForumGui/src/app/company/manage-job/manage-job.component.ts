import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Work} from '../../job/work';
import {ManageJobService} from './manage-job.service'
import {QuestionMakeService} from '../../discussion/question-make/question-make.service';
import { from } from 'rxjs';
import {Tag} from '../../discussion/model/tag';
@Component({
  selector: 'app-manage-job',
  templateUrl: './manage-job.component.html',
  styleUrls: ['./manage-job.component.css']
})
export class ManageJobComponent implements OnInit {

  isAdd= false;
  isEdit=false;
  disable = false;
  submittedAdd = false;
  submittedEdit = false;
  newWork : Work = new Work();
  editWork: Work = new Work();
  works : Work[] =[];
  worksView : Work[] =[];
  allTag : string[];

  isErrorTitleAdd = false;
  isErrorPositionAdd = false;
  isErrorAddressAdd = false;
  isErrorSalaryAdd = false;
  isErrorTagAdd= false;
  isErrorDescriptionAdd = false;

  titleAdd : string ='';
  positionAdd : string ='';
  addressAdd : string ='';
  salaryAdd : string ='';
  itemsAdd = [];
  descriptionAdd : string ='';
  tagAdds : Tag[] = [];

  constructor(
    private manageJobService: ManageJobService,
    private authenticationService : AuthenticationService,
    private formBuilder: FormBuilder,
    private questionMakeService :QuestionMakeService,
  ) { }

  ngOnInit() {
    this.getAllJob();
    this.getAllTag();
  }

  getAllJob(){
    this.manageJobService.getWorks()
      .subscribe(data => {
          this.works = data;
          this.worksView = this.works;
          this.worksView.sort((a:Work,b:Work) => {
            return <any>new Date(b.updationTime) - <any>new Date(a.updationTime);
          })
          console.log("get all job successful");
      })
  }

  addJob(){
    if(this.titleAdd.trim() == '' || (this.titleAdd.trim().length>150)){
      this.isErrorTitleAdd = true;
    }

    if(this.positionAdd.trim() == '' || (this.positionAdd.trim().length>150)){
      this.isErrorPositionAdd = true;
    }

    if(this.salaryAdd.trim() == '' || (this.salaryAdd.trim().length>150)){
      this.isErrorSalaryAdd = true;
    }

    if(this.addressAdd.trim() == '' || (this.addressAdd.trim().length>150)){
      this.isErrorAddressAdd = true;
    }

    if(this.itemsAdd.length == 0){
      this.isErrorTagAdd = true;
    }

    if(this.descriptionAdd.trim() == ''){
      this.isErrorDescriptionAdd = true;
    }

    if(this.isErrorTitleAdd == true || this.isErrorPositionAdd ==true || this.isErrorAddressAdd == true 
      || this.isErrorDescriptionAdd == true || this.isErrorSalaryAdd == true || this.isErrorTagAdd == true){
          return ;
      }

    this.authenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    this.newWork.companyId = this.authenticationService.currentUserInfo.id;
    this.newWork.title = this.titleAdd;
    this.newWork.description = this.descriptionAdd;
    this.newWork.address = this.addressAdd;
    this.newWork.position = this.positionAdd;
    this.newWork.salary = this.salaryAdd;

    this.itemsAdd.forEach(h=>{
      this.tagAdds.push(h);
    })
    this.newWork.tags = this.tagAdds;
    this.manageJobService.addWork(this.newWork)
    .subscribe(data => {
      console.log("addQuestion sucessfull");
      this.isAdd = false;
      this.works.push(data);
      this.worksView = this.works;
      this.worksView.sort((a:Work,b:Work) => {
        return <any>new Date(b.updationTime) - <any>new Date(a.updationTime);
      })
  })

    
  }

  getAllTag(){
    this.questionMakeService.getAllTag()
      .subscribe(data => {
        this.allTag = data;
        console.log("getAllTag succesfull");
      })
  }

  onTitleChangeAdd(event){
    if(this.titleAdd.trim() != ''){
      this.isErrorTitleAdd = false;
    }
  }

  onSalaryAddChangeAdd(event){
    if(this.salaryAdd.trim() != ''){
      this.isErrorSalaryAdd = false;
    }
  }
  onPositionChangeAdd(event){
    if(this.positionAdd.trim() != ''){
      this.isErrorPositionAdd = false;
    }
  }

  onAddressChangeAdd(event){
    if(this.addressAdd.trim() != ''){
      this.isErrorAddressAdd = false;
    }
  }

  onItemAdded(event){
    if(this.itemsAdd.length != 0){
      this.isErrorTagAdd = false;
    }

  }


  onTextChangeAdd(event){
    console.log(event);
    if(this.descriptionAdd.trim() != ''){
      console.log("khac null");
      this.isErrorDescriptionAdd = false;
    }
  }

  onSearchChange(value: string){
    if(value != ''){
      this.worksView = this.works.filter(u => u.title.toLowerCase().indexOf(value.toLowerCase())> -1);
    }else{
      this.worksView = this.works;
    }
  }

}
