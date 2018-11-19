import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileObjectiveComponent } from './profile-objective.component';

describe('ProfileObjectiveComponent', () => {
  let component: ProfileObjectiveComponent;
  let fixture: ComponentFixture<ProfileObjectiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileObjectiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
