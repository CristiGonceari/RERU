import { TestBed } from '@angular/core/testing';

import { ReferenceService } from './reference.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ReferenceService', () => {
  let service: ReferenceService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(ReferenceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
