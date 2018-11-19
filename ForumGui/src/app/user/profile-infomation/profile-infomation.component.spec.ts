import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileInfomationComponent } from './profile-infomation.component';

describe('ProfileInfomationComponent', () => {
  let component: ProfileInfomationComponent;
  let fixture: ComponentFixture<ProfileInfomationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfileInfomationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileInfomationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
