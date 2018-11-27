import { Component, OnInit } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { Subscription } from 'rxjs';
import {QuestionShowService} from './question-show.service';
import {Question} from '../model/question';
@Component({
  selector: 'app-question-show',
  templateUrl: './question-show.component.html',
  styleUrls: ['./question-show.component.css']
})
export class QuestionShowComponent implements OnInit {

  question : Question;
  id : string;
  subscription : Subscription;
  constructor(
    private activateRoute: ActivatedRoute,
    private questionShowService: QuestionShowService,
    private router : Router,
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
  }

  getQuestion(){
    this.questionShowService.getQuestionById(this.id)
      .subscribe(data => this.question = data);
  }
}
