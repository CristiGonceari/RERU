import { TestBed } from '@angular/core/testing';

import { ContractorTypeService } from './contractor-type.service';

describe('ContractorTypeService', () => {
  let service: ContractorTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContractorTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
