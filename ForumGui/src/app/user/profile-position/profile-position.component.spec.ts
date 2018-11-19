import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilePositionComponent } from './profile-position.component';

describe('ProfilePositionComponent', () => {
  let component: ProfilePositionComponent;
  let fixture: ComponentFixture<ProfilePositionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfilePositionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfilePositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
