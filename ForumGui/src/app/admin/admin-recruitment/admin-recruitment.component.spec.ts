import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminRecruitmentComponent } from './admin-recruitment.component';

describe('AdminRecruitmentComponent', () => {
  let component: AdminRecruitmentComponent;
  let fixture: ComponentFixture<AdminRecruitmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminRecruitmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminRecruitmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
