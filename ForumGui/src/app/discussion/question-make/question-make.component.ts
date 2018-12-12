import { Component, OnInit } from '@angular/core';
import {QuestionMakeService} from './question-make.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import {Question} from '../model/question';
import { Router, ActivatedRoute } from '@angular/router';
import {Tag} from '../model/tag'
@Component({
  selector: 'app-question-make',
  templateUrl: './question-make.component.html',
  styleUrls: ['./question-make.component.css']
})
export class QuestionMakeComponent implements OnInit {

  submitted = false;
  items =  [];
  discription : string = "";
  title : string = '';
  errorTitle: string = '';
  errorTag: string = '';
  errorDiscription: string = '';
  //tags:string[] = [];
  tags : Tag[] = [];
  allTag : string[];
  newQuestion : Question = new Question();
  constructor(
    private questionMakeService: QuestionMakeService,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
  ) { 
  }

  ngOnInit() {
    this.getAllTag();
  }


  onSubmit(){
    
    if(this.title == '' || (this.title.length>150)){
      this.errorTitle = "Sorry, the title is not empty and length is not greater than 150";
    }

    if(this.items.length == 0){
      this.errorTag = "Sorry, the list tags is not empty";
    }

    if(this.discription == null || this.discription.trim() == ''){
      this.errorDiscription = "Sorry, the discription is not empty";
    }
    if(this.errorTitle != '' || this.errorTag != '' || this.errorDiscription != ''){
      console.log("error rồi cmn");
      console.log(this.errorTitle + this.errorTag + this.errorDiscription);
      return;
    }

    let userId = this.authenticationService.getCurrentUser().id;
    this.newQuestion.userId = userId;
    this.newQuestion.title = this.title;
    this.newQuestion.description = this.discription;
    console.log("discription: "+ this.newQuestion.description);

    this.items.forEach(h=>{
      this.tags.push(h);
    })
    this.newQuestion.tags = this.tags;
    console.log("tag: " + this.tags);
    this.questionMakeService.addQuestion(this.newQuestion)
      .subscribe(data => {
        console.log("addQuestion sucessfull");
        this.router.navigate(['/home']);
    })
  }
    

  onItemAdded(event){
    console.log("this.iteams: ");   
    console.log(this.items);
    if(this.items.length != 0){
      this.errorTag = '';
    }

  }

  onTextChange(event){
    this.discription = event.htmlValue;
    console.log("this.discription: "+this.discription);
    console.log("discription text: "+event);
    if(this.discription != ''){
      this.errorDiscription = '';
    }
  }
  
  onSearchChange(searchValue : string){
    //console.log("this.discription: "+this.discription);
    console.log("input text: "+ searchValue);
    if(this.title != '' && (this.title.length<=150)){
      this.errorTitle = '';
    }
    if(this.title == '' || (this.title.length>150)){
      this.errorTitle = "Sorry, the title is not empty and length is not greater than 150";
    }
  }

  getAllTag(){
    this.questionMakeService.getAllTag()
      .subscribe(data => {
        this.allTag = data;
        console.log("getAllTag succesfull");
      })
  }
}
