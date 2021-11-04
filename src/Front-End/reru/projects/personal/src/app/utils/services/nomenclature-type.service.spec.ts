import { TestBed } from '@angular/core/testing';

import { NomenclatureTypeService } from './nomenclature-type.service';

describe('NomenclatureTypeService', () => {
  let service: NomenclatureTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NomenclatureTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
