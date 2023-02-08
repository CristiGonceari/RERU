import { TestBed } from '@angular/core/testing';

import { LocationResponsiblePersonService } from './location-responsible-person.service';

describe('LocationResponsiblePersonService', () => {
  let service: LocationResponsiblePersonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocationResponsiblePersonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
