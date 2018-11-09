import { Component, OnInit } from '@angular/core';
import {ConfigService} from '../config.service';


@Component({
  selector: 'app-input-format',
  templateUrl: './input-format.component.html',
  styleUrls: ['./input-format.component.css']
})
export class InputFormatComponent implements OnInit {

  editorConfig ={};
  
  constructor(private configService: ConfigService) { 
    this.editorConfig = configService.editorConfig;
  }

  ngOnInit() {
  }

}
