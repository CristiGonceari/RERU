import { TestBed } from '@angular/core/testing';

import { NomenclatureRecordService } from './nomenclature-record.service';

describe('NomenclatureRecordService', () => {
  let service: NomenclatureRecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NomenclatureRecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
