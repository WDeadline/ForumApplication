import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageRecruitmentComponent } from './manage-recruitment.component';

describe('ManageRecruitmentComponent', () => {
  let component: ManageRecruitmentComponent;
  let fixture: ComponentFixture<ManageRecruitmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageRecruitmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageRecruitmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
