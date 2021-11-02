import { TestBed } from '@angular/core/testing';

import { ContractorProfileService } from './contractor-profile.service';

describe('ContractorProfileService', () => {
  let service: ContractorProfileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContractorProfileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
