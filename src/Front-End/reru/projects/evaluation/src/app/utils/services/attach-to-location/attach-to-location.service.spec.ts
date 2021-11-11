import { TestBed } from '@angular/core/testing';

import { AttachToLocationService } from './attach-to-location.service';

describe('AttachToLocationService', () => {
  let service: AttachToLocationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AttachToLocationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
