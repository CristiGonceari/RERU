import { TestBed } from '@angular/core/testing';

import { CandidatePositionService } from './candidate-position.service';

describe('CandidatePositionService', () => {
  let service: CandidatePositionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CandidatePositionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
