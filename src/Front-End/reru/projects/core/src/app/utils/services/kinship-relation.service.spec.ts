import { TestBed } from '@angular/core/testing';

import { KinshipRelationService } from './kinship-relation.service';

describe('KinshipRelationService', () => {
  let service: KinshipRelationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KinshipRelationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
