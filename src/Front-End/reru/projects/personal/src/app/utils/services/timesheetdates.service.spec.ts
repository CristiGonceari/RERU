import { TestBed } from '@angular/core/testing';

import { TimesheetdatesService } from './timesheetdates.service';

describe('TimesheetdatesService', () => {
  let service: TimesheetdatesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TimesheetdatesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
