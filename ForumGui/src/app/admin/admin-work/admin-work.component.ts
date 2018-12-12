import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-work',
  templateUrl: './admin-work.component.html',
  styleUrls: ['./admin-work.component.css']
})
export class AdminWorkComponent implements OnInit {

  isAdd= false;
  isEdit=false;
  disable = false;
  constructor() { }

  ngOnInit() {
  }

}
