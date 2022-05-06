import { TestBed } from '@angular/core/testing';

import { TryLongRequestService } from './try-long-request.service';

describe('TryLongRequestService', () => {
  let service: TryLongRequestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TryLongRequestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
