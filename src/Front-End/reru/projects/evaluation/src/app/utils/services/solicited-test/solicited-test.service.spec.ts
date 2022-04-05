import { TestBed } from '@angular/core/testing';

import { SolicitedTestService } from './solicited-test.service';

describe('SolicitedTestService', () => {
  let service: SolicitedTestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SolicitedTestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
