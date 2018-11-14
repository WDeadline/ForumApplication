import { Component, OnInit } from '@angular/core';
import {ConfigService} from '../config.service';
@Component({
  selector: 'app-question-show',
  templateUrl: './question-show.component.html',
  styleUrls: ['./question-show.component.css']
})
export class QuestionShowComponent implements OnInit {

  editorConfig ={};
  
  constructor(private configService: ConfigService) { 
    this.editorConfig = configService.editorConfig;
  }
  ngOnInit() {
  }

}
