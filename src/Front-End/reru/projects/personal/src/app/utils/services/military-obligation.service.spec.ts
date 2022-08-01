import { TestBed } from '@angular/core/testing';

import { MilitaryObligationService } from './military-obligation.service';

describe('MilitaryObligationService', () => {
  let service: MilitaryObligationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MilitaryObligationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
