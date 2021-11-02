import { TestBed } from '@angular/core/testing';

import { RequestsProfileService } from './requests-profile.service';

describe('RequestsProfileService', () => {
  let service: RequestsProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RequestsProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
