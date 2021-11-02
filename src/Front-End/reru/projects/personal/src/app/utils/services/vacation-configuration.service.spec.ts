import { TestBed } from '@angular/core/testing';

import { VacationConfigurationService } from './vacation-configuration.service';

describe('VacationConfigurationService', () => {
  let service: VacationConfigurationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VacationConfigurationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
