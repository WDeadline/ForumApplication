import { Component, OnInit} from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {QuestionShowService} from './question-show.service';
import {Question} from '../model/question';
import {Answer} from '../model/answer';
import {Comment} from '../model/comment';
import {AuthenticationService} from '../../authentication/service/authentication.service';
@Component({
  selector: 'app-question-show',
  templateUrl: './question-show.component.html',
  styleUrls: ['./question-show.component.css'],
})
export class QuestionShowComponent implements OnInit {

  isAddComment = false;
  isEditComment = false;

  isAddAnswer = false;
  isEditAnswer = false;

  question : Question = new Question();
  curentAnswerId: string = "";
  curentCommentId: string = "";
  imageURL: string = "assets/img/default.png";
  id : string;
  answerId: string;

  content : string = "";
  contentEdit : string = "";
  comment : string = "";
  commentEdit : string = "";
  subscription : Subscription;

  errorContent: string = "";
  errorContentEdit: string = "";
  errorComment: string = "";
  errorCommentEdit: string = "";

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
    console.log("this.discription: "+ this.content);
    console.log(event );
    if(this.content != null && this.content.trim() != ''){
      this.errorContent = '';
      return;
    }
  }

  onTextChangeEdit(event){
    this.contentEdit = event.htmlValue;
    console.log("this.discription: "+this.contentEdit);
    console.log(event);
    if(this.contentEdit != null && this.contentEdit.trim() != ''){
      this.errorContentEdit = '';
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

  editAnswer(answer: Answer){
    this.contentEdit = answer.content;
    this.answerId = answer.id;
  }

  putAnswer(){
    if(this.authenticationService.currentUserInfo == null){
      this.router.navigate(['/login'], {queryParams: {returnUrl : '/showquestion', returnParamId: this.question.id}});
      return;
    }
    if(this.contentEdit == null || this.contentEdit.trim() == ''){
      this.errorContent = "Please enter your answer to edit";
      return;
    }
    console.log(this.errorContent);
    this.questionShowService.updateAnswer(this.id, this.answerId, this.contentEdit)
      .subscribe(data => {
        var index = this.question.answers.findIndex(a => a.id == this.answerId);
        this.question.answers[index] = data;
        this.isEditAnswer = false;
        console.log("editQuestion sucessfull");
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
        this.isAddComment = false;
        this.comment = '';
        console.log("postQuestion sucessfull");
      });
  }

  public editComment(comment: Comment){
    this.commentEdit = comment.content;
    this.curentCommentId = comment.id;
  }

  putComment(){
    console.log(this.curentCommentId);  
    console.log(this.curentAnswerId);
    if(this.authenticationService.currentUserInfo == null){
      this.router.navigate(['/login'], {queryParams: {returnUrl : '/showquestion', returnParamId: this.question.id}});
      return;
    }
    if(this.commentEdit == null || this.commentEdit.trim() == ''){
      this.errorCommentEdit = "Enter your comment for edit";
      return;
    }
    this.questionShowService.updateComment(this.id, this.curentAnswerId, this.curentCommentId, this.commentEdit)
    .subscribe(data => {
        var index = this.question.answers.findIndex(a => a.id == this.curentAnswerId);
        var indexComment = this.question.answers[index].comments.findIndex(c => c.id == this.curentCommentId);
        this.question.answers[index].comments[indexComment] = data;
        this.isEditComment = false;
        this.commentEdit = '';
        console.log("postQuestion sucessfull");
      })

  }

  deleteComment(answerId: string, commentId: string){
    var index = this.question.answers.findIndex(a => a.id == answerId);
    this.question.answers[index].comments = this.question.answers[index].comments.filter(c => c.id != commentId);
    this.questionShowService.deleteComment(this.id, answerId, commentId).subscribe(data => {console.log("delete successful")});
  }

}
