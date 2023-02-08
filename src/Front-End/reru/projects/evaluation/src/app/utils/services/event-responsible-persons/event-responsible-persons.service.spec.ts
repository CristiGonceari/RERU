import { TestBed } from '@angular/core/testing';

import { EventResponsiblePersonsService } from './event-responsible-persons.service';

describe('EventResponsiblePersonsService', () => {
  let service: EventResponsiblePersonsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventResponsiblePersonsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
