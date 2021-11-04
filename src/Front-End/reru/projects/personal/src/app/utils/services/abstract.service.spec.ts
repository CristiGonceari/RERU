import { TestBed } from '@angular/core/testing';

import { AbstractService } from './abstract.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AbstractService', () => {
  let service: AbstractService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(AbstractService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
