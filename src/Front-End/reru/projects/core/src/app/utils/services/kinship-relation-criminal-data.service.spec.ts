import { TestBed } from '@angular/core/testing';

import { KinshipRelationCriminalDataService } from './kinship-relation-criminal-data.service';

describe('KinshipRelationCriminalDataService', () => {
  let service: KinshipRelationCriminalDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KinshipRelationCriminalDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
