import { Component, OnInit } from '@angular/core';
import {QuestionMakeService} from './question-make.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../../authentication/service/authentication.service';
import {Question} from '../model/question';
import { Router, ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-question-make',
  templateUrl: './question-make.component.html',
  styleUrls: ['./question-make.component.css']
})
export class QuestionMakeComponent implements OnInit {

  submitted = false;
  items =  [{value: 'dsdad', display: 'Angular'}, {value: 'dsdsda', display: 'React'}];
  discription : string = "";
  title : string;
  errorTitle:string;
  errorTag: string;
  errorDiscription: string;
  tags:string[] = [];
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

  }


  onSubmit(){
    if(this.title == '' || (this.title.length>150)){
      this.errorTitle = "Sorry, the title is not empty and length is not greater than 150";
      return;
    }


    if(this.tags.length == 0){
      this.errorTag = "Sorry, the list tags is not empty";
      return;
    }

    if(this.discription == ''){
      this.errorDiscription = "Sorry, the discription is not empty";
      return;
    }

    let userId = this.authenticationService.getCurrentUser().id;
    this.newQuestion.userId = userId;
    this.newQuestion.title = this.title;
    this.newQuestion.description = this.discription;
    console.log("discription: "+ this.newQuestion.description);
    this.newQuestion.tags = this.tags;

    this.questionMakeService.addQuestion(this.newQuestion)
      .subscribe(data => {
        console.log("addQuestion sucessfull");
        this.router.navigate(['/home']);
    })
  }
    

  onItemAdded(event){   
    console.log("this.iteams: "+ event.value);
    this.tags.push(event.value);
  }

  onTextChange(event){
    console.log("this.discription: "+this.discription);
    console.log("discription text: "+event);
  }
  
}
