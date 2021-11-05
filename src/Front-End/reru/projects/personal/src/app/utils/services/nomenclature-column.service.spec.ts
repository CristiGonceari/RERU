import { TestBed } from '@angular/core/testing';

import { NomenclatureColumnService } from './nomenclature-column.service';

describe('NomenclatureColumnService', () => {
  let service: NomenclatureColumnService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NomenclatureColumnService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
