import { TestBed } from '@angular/core/testing';

import { SolicitedVacantPositionUserFileService } from './solicited-vacant-position-user-file.service';

describe('SolicitedVacantPositionUserFileService', () => {
  let service: SolicitedVacantPositionUserFileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SolicitedVacantPositionUserFileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
