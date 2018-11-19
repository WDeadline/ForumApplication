import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile-skill',
  templateUrl: './profile-skill.component.html',
  styleUrls: ['./profile-skill.component.css']
})
export class ProfileSkillComponent implements OnInit {

  isEdit = false;
  isAdd =false;
  constructor() { }

  ngOnInit() {
  }

}
