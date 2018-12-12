import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute,Router } from '@angular/router';
import {AuthenticationService} from '../authentication/service/authentication.service';
import {JobDetailService} from './job-detail.service';
import {Work} from '../job/work';
@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.css']
})
export class JobDetailComponent implements OnInit {

  subscription : Subscription;
  id: string ='';
  work: Work = new Work();
  constructor(
    private activateRoute: ActivatedRoute,
    private router : Router,
    private authenticationService: AuthenticationService,
    private jobDetailService:JobDetailService,
  ) { }

  ngOnInit() {
    this.subscription = this.activateRoute.queryParams.subscribe(val=>{
      if (val['id'])
        this.id = val['id'];
        if(this.id == null)
        {
          this.router.navigate(['/home']);
        } 
    });
    this.getQuestion();
  }

  getQuestion(){
    this.jobDetailService.getWorkById(this.id)
      .subscribe(data => {
        this.work = data;     
      });
  }

}
