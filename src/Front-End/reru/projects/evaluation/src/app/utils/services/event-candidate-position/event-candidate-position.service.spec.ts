import { TestBed } from '@angular/core/testing';

import { EventCandidatePositionService } from './event-candidate-position.service';

describe('EventCandidatePositionService', () => {
  let service: EventCandidatePositionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventCandidatePositionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
