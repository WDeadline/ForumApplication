import { TestBed } from '@angular/core/testing';

import { ProfileActivityService } from './profile-activity.service';

describe('ProfileActivityService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProfileActivityService = TestBed.get(ProfileActivityService);
    expect(service).toBeTruthy();
  });
});
