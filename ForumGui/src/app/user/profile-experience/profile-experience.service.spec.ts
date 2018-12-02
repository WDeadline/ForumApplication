import { TestBed } from '@angular/core/testing';

import { ProfileExperienceService } from './profile-experience.service';

describe('ProfileExperienceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProfileExperienceService = TestBed.get(ProfileExperienceService);
    expect(service).toBeTruthy();
  });
});
