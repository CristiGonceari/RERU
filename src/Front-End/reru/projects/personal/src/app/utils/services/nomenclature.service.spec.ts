import { TestBed } from '@angular/core/testing';

import { NomenclatureService } from './nomenclature.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('NomenclatureService', () => {
  let service: NomenclatureService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(NomenclatureService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
