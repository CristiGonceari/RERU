import { TestBed } from '@angular/core/testing';

import { ContractorService } from './contractor.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ContractorService', () => {
  let service: ContractorService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(ContractorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
