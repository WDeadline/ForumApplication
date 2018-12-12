import { Component, OnInit} from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {QuestionShowService} from './question-show.service';
import {Question} from '../model/question';
import {Answer} from '../model/answer';
import {AuthenticationService} from '../../authentication/service/authentication.service';
@Component({
  selector: 'app-question-show',
  templateUrl: './question-show.component.html',
  styleUrls: ['./question-show.component.css'],
})
export class QuestionShowComponent implements OnInit {

  isComment = false;
  isAddComment = true;
  question : Question = new Question();
  curentAnswerId: string = "";
  imageURL: string = "assets/img/default.png";
  id : string;
  content : string = "";
  comment : string = "";
  subscription : Subscription;
  errorContent: string = "";
  errorComment: string = "";
  postAnwer : Answer = new Answer();
  constructor(
    private activateRoute: ActivatedRoute,
    private questionShowService: QuestionShowService,
    private router : Router,
    private authenticationService: AuthenticationService,
  ) { 
  }
  ngOnInit() {
    this.authenticationService.currentUserInfo = JSON.parse(localStorage.getItem('currentUser'));
    this.subscription = this.activateRoute.queryParams.subscribe(val=>{
      if (val['id'])
        this.id = val['id'];
        if(this.id == null)
        {
          this.router.navigate(['/home']);
        } 
    });
    this.getQuestion();
    
  }

  getQuestion(){
    this.questionShowService.getQuestionById(this.id)
      .subscribe(data => {
        this.question = data;
      });
  }

  onTextChange(event){
    this.content = event.htmlValue;
    console.log("this.discription: "+this.content);
    console.log(event );
    if(this.content != null && this.content.trim() != ''){
      this.errorContent = '';
      return;
    }
  }

  postAnswer(){
    if(this.authenticationService.currentUserInfo == null){
      this.router.navigate(['/login'], {queryParams: {returnUrl : '/showquestion', returnParamId: this.question.id}});
      return;
    }
    if(this.content == null || this.content.trim() == ''){
      this.errorContent = "true";
      return;
    }
    this.questionShowService.addAnswer(this.content, this.id)
      .subscribe(data => {
        this.question.answers.push(data);
        this.content = null;
        console.log("postQuestion sucessfull");
      })
  }

  deleteAnwer(answerId: string){
    this.question.answers = this.question.answers.filter(f => f.id != answerId);
    this.questionShowService.deleteAnswer(this.id, answerId).subscribe(data => {console.log("delete successful")});
  }

  postComment(answerId: string){
    if(this.authenticationService.currentUserInfo == null){
      this.router.navigate(['/login'], {queryParams: {returnUrl : '/showquestion', returnParamId: this.question.id}});
      return;
    }
    if(this.comment == null || this.comment.trim() == ''){
      this.errorComment = "Enter your comment";
      return;
    }
    this.questionShowService.addComment(this.id, answerId, this.comment)
    .subscribe(data => {
        var index = this.question.answers.findIndex(a => a.id == answerId);
        this.question.answers[index].comments.push(data);
        this.isComment = false;
        this.comment = '';
        console.log("postQuestion sucessfull");
      })
  }

  deleteComment(answerId: string, commentId: string){
    var index = this.question.answers.findIndex(a => a.id == answerId);
    this.question.answers[index].comments = this.question.answers[index].comments.filter(c => c.id != commentId);
    this.questionShowService.deleteComment(this.id, answerId, commentId).subscribe(data => {console.log("delete successful")});
  }

}
