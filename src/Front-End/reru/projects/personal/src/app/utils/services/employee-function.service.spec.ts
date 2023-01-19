import { TestBed } from '@angular/core/testing';

import { EmployeeFunctionService } from './employee-function.service';

describe('EmployeeFunctionService', () => {
  let service: EmployeeFunctionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmployeeFunctionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
