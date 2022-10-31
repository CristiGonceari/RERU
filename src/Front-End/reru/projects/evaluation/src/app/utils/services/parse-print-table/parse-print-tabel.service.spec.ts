import { TestBed } from '@angular/core/testing';

import { ParsePrintTabelService } from './parse-print-tabel.service';

describe('ParsePrintTabelService', () => {
  let service: ParsePrintTabelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ParsePrintTabelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
