import { TestBed } from '@angular/core/testing';

import { InternalGetTestIdService } from './internal-get-test-id.service';

describe('InternalGetTestIdService', () => {
  let service: InternalGetTestIdService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InternalGetTestIdService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
