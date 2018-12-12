import { Component, OnInit } from '@angular/core';
import {JobService} from './job.service';
import {Work} from './work';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AuthenticationService} from '../authentication/service/authentication.service';
@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent implements OnInit {

  works : Work[] =[];
  constructor(
    private jobService : JobService,
  ) { }

  ngOnInit() {
    this.getJobs();
  }

  getJobs(){
    this.jobService.getWorks()
      .subscribe(data => {
        this.works = data;
        console.log("get jobs successful");
      });
  }


}
