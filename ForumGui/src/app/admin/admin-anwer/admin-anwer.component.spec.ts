import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAnwerComponent } from './admin-anwer.component';

describe('AdminAnwerComponent', () => {
  let component: AdminAnwerComponent;
  let fixture: ComponentFixture<AdminAnwerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminAnwerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminAnwerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
