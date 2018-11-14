import { Component, OnInit } from '@angular/core';
import {Test} from './test';
import {TestService} from './test.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  tests : Test[];
  constructor(private testService : TestService) { }

  ngOnInit() {
    this.getTests();
  }

  getTests(): void {
    this.testService.getTests().subscribe(x => this.tests = x);
  }

}
