import { TestBed } from '@angular/core/testing';

import { VacationProfileService } from './vacation-profile.service';

describe('VacationProfileService', () => {
  let service: VacationProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VacationProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
