import { TestBed } from '@angular/core/testing';

import { ProfileSkillService } from './profile-skill.service';

describe('ProfileSkillService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProfileSkillService = TestBed.get(ProfileSkillService);
    expect(service).toBeTruthy();
  });
});
