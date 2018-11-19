import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile-objective',
  templateUrl: './profile-objective.component.html',
  styleUrls: ['./profile-objective.component.css']
})
export class ProfileObjectiveComponent implements OnInit {

  isEdit = false;
  isAdd = false;
  constructor() { }

  ngOnInit() {
  }

}
