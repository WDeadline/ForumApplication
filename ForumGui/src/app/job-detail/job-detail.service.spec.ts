import { TestBed } from '@angular/core/testing';

import { JobDetailService } from './job-detail.service';

describe('JobDetailService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: JobDetailService = TestBed.get(JobDetailService);
    expect(service).toBeTruthy();
  });
});
