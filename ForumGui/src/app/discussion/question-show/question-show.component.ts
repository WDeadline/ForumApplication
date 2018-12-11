import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {QuestionShowService} from './question-show.service';
import {Question} from '../model/question';
import {Anwer} from '../model/anwer';
import {AuthenticationService} from '../../authentication/service/authentication.service';
@Component({
  selector: 'app-question-show',
  templateUrl: './question-show.component.html',
  styleUrls: ['./question-show.component.css']
})
export class QuestionShowComponent implements OnInit {

  isComment = false;
  isAddComment = true;
  question : Question= new Question();
  anwers : Anwer[] = []; 
  id : string;
  description : string = "";
  subscription : Subscription;
  errorPostAnwer = false;
  postAnwer : Anwer = new Anwer();
  constructor(
    private activateRoute: ActivatedRoute,
    private questionShowService: QuestionShowService,
    private router : Router,
    private authenticationService: AuthenticationService,
  ) { 
  }
  ngOnInit() {

    this.subscription = this.activateRoute.queryParams.subscribe(val=>{
      if (val['id'])
        this.id = val['id'];
        if(this.id == null)
        {
          this.router.navigate(['/home']);
        } 
    });

    this.getQuestion();
    console.log("on innit");
    console.log(this.question);
    this.getAnwers();
  }

  getQuestion(){
    this.questionShowService.getQuestionById(this.id)
      .subscribe(data => {
        this.question = data;
        console.log("on get");
        console.log(this.question);
      
      });
  }

  getAnwers(){
    this.questionShowService.getAnwerByQuestion(this.id)
      .subscribe(data => {
        this.anwers = data;
        console.log("get anwer successful");        
      }) 
  }

  onTextChange(event){
    console.log("this.discription: "+this.description);
    console.log("discription text: "+ event );
    if(this.description != ''){
     //this.errorDiscription = '';
    }
  }

  postQuestion(){

    this.authenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    if(this.description.trim() == ''){
      this.errorPostAnwer = true;
    }
    this.postAnwer.content = this.description;
    this.postAnwer.userView.avatar = this.authenticationService.currentUserInfo.avatar;
    this.postAnwer.userView.id = this.authenticationService.currentUserInfo.id;
    this.postAnwer.userView.name = this.authenticationService.currentUserInfo.name;

    this.questionShowService.addAnswer(this.postAnwer, this.id)
      .subscribe(data => {
        this.anwers.push(data);
        console.log("postQuestion sucessfull");
      })
  }


}
