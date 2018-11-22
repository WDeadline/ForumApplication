import { TestBed } from '@angular/core/testing';

import { ProfileObjectiveService } from './profile-objective.service';

describe('ProfileObjectiveService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ProfileObjectiveService = TestBed.get(ProfileObjectiveService);
    expect(service).toBeTruthy();
  });
});
