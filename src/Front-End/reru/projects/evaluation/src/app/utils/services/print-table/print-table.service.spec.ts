import { TestBed } from '@angular/core/testing';

import { PrintTableService } from './print-table.service';

describe('PrintTableService', () => {
  let service: PrintTableService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PrintTableService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
