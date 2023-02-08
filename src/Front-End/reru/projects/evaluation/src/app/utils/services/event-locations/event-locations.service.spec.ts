import { TestBed } from '@angular/core/testing';

import { EventLocationsService } from './event-locations.service';

describe('EventLocationsService', () => {
  let service: EventLocationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventLocationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
