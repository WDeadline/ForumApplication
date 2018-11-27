import { TestBed } from '@angular/core/testing';

import { QuestionShowService } from './question-show.service';

describe('QuestionShowService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: QuestionShowService = TestBed.get(QuestionShowService);
    expect(service).toBeTruthy();
  });
});
