import { TestBed } from '@angular/core/testing';

import { InitializerUserProfileService } from './initializer-user-profile.service';

describe('InitializerUserProfileService', () => {
  let service: InitializerUserProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InitializerUserProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
