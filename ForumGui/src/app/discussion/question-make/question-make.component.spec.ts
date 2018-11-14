import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionMakeComponent } from './question-make.component';

describe('QuestionMakeComponent', () => {
  let component: QuestionMakeComponent;
  let fixture: ComponentFixture<QuestionMakeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestionMakeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionMakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
