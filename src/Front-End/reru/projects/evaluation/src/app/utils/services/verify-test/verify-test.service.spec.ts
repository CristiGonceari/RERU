import { TestBed } from '@angular/core/testing';

import { VerifyTestService } from './verify-test.service';

describe('VerifyTestService', () => {
  let service: VerifyTestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VerifyTestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
