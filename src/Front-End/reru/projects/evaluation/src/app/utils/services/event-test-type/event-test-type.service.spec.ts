import { TestBed } from '@angular/core/testing';

import { EventTestTypeService } from './event-test-type.service';

describe('EventTestTypeService', () => {
  let service: EventTestTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventTestTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
