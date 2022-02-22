import { TestBed } from '@angular/core/testing';

import { EventTestTemplateService } from './event-test-template.service';

describe('EventTestTemplateService', () => {
  let service: EventTestTemplateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventTestTemplateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
