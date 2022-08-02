import { TestBed } from '@angular/core/testing';

import { MaterialStatusService } from './material-status.service';

describe('MaterialStatusService', () => {
  let service: MaterialStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MaterialStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
