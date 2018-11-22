import { TestBed } from '@angular/core/testing';

import { QuestionMakeService } from './question-make.service';

describe('QuestionMakeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QuestionMakeService = TestBed.get(QuestionMakeService);
    expect(service).toBeTruthy();
  });
});
