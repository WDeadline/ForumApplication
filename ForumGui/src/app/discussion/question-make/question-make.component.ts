import { Component, OnInit } from '@angular/core';
import {ConfigService} from '../config.service';
@Component({
  selector: 'app-question-make',
  templateUrl: './question-make.component.html',
  styleUrls: ['./question-make.component.css']
})
export class QuestionMakeComponent implements OnInit {

  editorConfig ={};
  
  constructor(private configService: ConfigService) { 
    this.editorConfig = configService.editorConfig;
  }

  ngOnInit() {
  }

}
