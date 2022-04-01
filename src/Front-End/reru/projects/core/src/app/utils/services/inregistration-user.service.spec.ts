import { TestBed } from '@angular/core/testing';

import { InregistrationUserService } from './inregistration-user.service';

describe('InregistrationUserService', () => {
  let service: InregistrationUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InregistrationUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
