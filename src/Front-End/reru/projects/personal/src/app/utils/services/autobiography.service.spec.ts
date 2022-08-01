import { TestBed } from '@angular/core/testing';

import { AutobiographyService } from './autobiography.service';

describe('AutobiographyService', () => {
  let service: AutobiographyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AutobiographyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
