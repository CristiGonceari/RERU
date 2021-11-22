import { TestBed } from '@angular/core/testing';

import { TestVerificationProcessService } from './test-verification-process.service';

describe('TestVerificationProcessService', () => {
  let service: TestVerificationProcessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestVerificationProcessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
