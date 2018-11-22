import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-input-format',
  templateUrl: './input-format.component.html',
  styleUrls: ['./input-format.component.css']
})
export class InputFormatComponent implements OnInit {

  editorConfig ={};
  text: string;
  constructor() { 
  }

  ngOnInit() {
  }

}
