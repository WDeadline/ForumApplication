import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InputFormatComponent } from './input-format.component';

describe('InputFormatComponent', () => {
  let component: InputFormatComponent;
  let fixture: ComponentFixture<InputFormatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InputFormatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InputFormatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
