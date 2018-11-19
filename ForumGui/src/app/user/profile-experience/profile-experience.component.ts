import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile-experience',
  templateUrl: './profile-experience.component.html',
  styleUrls: ['./profile-experience.component.css']
})
export class ProfileExperienceComponent implements OnInit {

  isEdit=false;
  isAdd = false;
  constructor() { }

  ngOnInit() {
  }

}
