import { TestBed } from '@angular/core/testing';

import { CandidatePositionNotificationService } from './candidate-position-notification.service';

describe('CandidatePositionNotificationService', () => {
  let service: CandidatePositionNotificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CandidatePositionNotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
