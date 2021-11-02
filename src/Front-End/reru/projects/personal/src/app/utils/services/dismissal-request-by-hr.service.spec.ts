import { TestBed } from '@angular/core/testing';

import { DismissalRequestByHrService } from './dismissal-request-by-hr.service';

describe('DismissalRequestByHrService', () => {
  let service: DismissalRequestByHrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DismissalRequestByHrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
