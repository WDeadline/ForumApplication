import { TestBed } from '@angular/core/testing';

import { ProfileEducationService } from './profile-education.service';

describe('ProfileEducationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProfileEducationService = TestBed.get(ProfileEducationService);
    expect(service).toBeTruthy();
  });
});
