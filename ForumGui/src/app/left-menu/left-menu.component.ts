import { Component, OnInit } from '@angular/core';
import {JobService} from '../job/job.service';
import {Work} from '../job/work';
@Component({
  selector: 'app-left-menu',
  templateUrl: './left-menu.component.html',
  styleUrls: ['./left-menu.component.css']
})
export class LeftMenuComponent implements OnInit {

  works : Work[] = [];
  constructor(
    private jobService : JobService,
  ) { }

  ngOnInit() {
    this.getAllJobs();
  }

  getAllJobs(){
    this.jobService.getWorks()
      .subscribe(data =>{
        for(var i =0; i < 5 && i < data.length; i++){
          this.works[i] = data[i];
        }
      });
  }

}
