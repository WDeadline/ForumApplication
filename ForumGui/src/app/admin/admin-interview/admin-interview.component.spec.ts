import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminInterviewComponent } from './admin-interview.component';

describe('AdminInterviewComponent', () => {
  let component: AdminInterviewComponent;
  let fixture: ComponentFixture<AdminInterviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminInterviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminInterviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
