import { TestBed } from '@angular/core/testing';

import { TimeSheetTableValuesService } from './time-sheet-table-values.service';

describe('TimeSheetTableValuesService', () => {
  let service: TimeSheetTableValuesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TimeSheetTableValuesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
