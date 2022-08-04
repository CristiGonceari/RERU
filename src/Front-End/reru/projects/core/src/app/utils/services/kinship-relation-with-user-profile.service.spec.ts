import { TestBed } from '@angular/core/testing';

import { KinshipRelationWithUserProfileService } from './kinship-relation-with-user-profile.service';

describe('KinshipRelationWithUserProfileService', () => {
  let service: KinshipRelationWithUserProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KinshipRelationWithUserProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
