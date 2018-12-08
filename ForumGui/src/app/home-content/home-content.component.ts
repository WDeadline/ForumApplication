import { Component, OnInit } from '@angular/core';
import {HomeService} from './home.service';
import {Question} from '../discussion/model/question';
const now = new Date();
@Component({
  selector: 'app-home-content',
  templateUrl: './home-content.component.html',
  styleUrls: ['./home-content.component.css']
})
export class HomeContentComponent implements OnInit {
 
  questions : Question[];
  constructor(
    private homeService: HomeService,
  ) { }

  ngOnInit() {
    this.getQuestions();
  }

  getQuestions(): void {
    this.homeService.getQuestions()
    .subscribe(questions => {
      console.log("lengh:" + questions[0].tags.length);
      this.questions = questions;
      this.questions.sort((a:Question,b:Question) => {
        return <any>new Date(b.updationTime) - <any>new Date(a.updationTime);
      })
      
    });
  }

  getQuestionsOfMonth(): void {
    this.homeService.getQuestions()
    .subscribe(questions => {
      this.questions = this.sortQuestionsOfMonth(questions);
    });
  }

  getQuestionsOfYeah(): void {
    this.homeService.getQuestions()
    .subscribe(questions => {
      this.questions = this.sortQuestionsOfYeah(questions);
    });
  }

  sortQuestionsOfMonth(questions: Question[] ){ 
    let questionsResult: Array<Question> = [];
    let month = now.getMonth()+1;
    questions.forEach(question => {
      let dateCreated = new Date(question.updationTime);
      let monthCreated = dateCreated.getMonth()+1;   
      if (monthCreated == month) {
        questionsResult.push(question);
      }
    });
    return questionsResult;
  }

  sortQuestionsOfYeah(questions: Question[] ){ 
    let questionsResult: Array<Question> = [];
    let currentYeah = now.getFullYear();
    questions.forEach(question => {
      let dateCreated = new Date(question.updationTime);
      let yeahCreated = dateCreated.getFullYear();   
      if (yeahCreated == currentYeah) {
        questionsResult.push(question);
      }
    });
    return questionsResult;
  }
 
}
