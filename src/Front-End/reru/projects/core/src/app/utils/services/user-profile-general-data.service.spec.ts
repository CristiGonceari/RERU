import { TestBed } from '@angular/core/testing';

import { UserProfileGeneralDataService } from './user-profile-general-data.service';

describe('UserProfileGeneralDataService', () => {
  let service: UserProfileGeneralDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserProfileGeneralDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
