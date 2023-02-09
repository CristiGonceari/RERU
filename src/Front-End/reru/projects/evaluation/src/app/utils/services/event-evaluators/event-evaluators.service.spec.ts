import { TestBed } from '@angular/core/testing';

import { EventEvaluatorsService } from './event-evaluators.service';

describe('EventEvaluatorsService', () => {
  let service: EventEvaluatorsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventEvaluatorsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
