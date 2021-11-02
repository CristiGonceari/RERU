import { TestBed } from '@angular/core/testing';

import { OrganigramService } from './organigram.service';

describe('OrganigramService', () => {
  let service: OrganigramService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OrganigramService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
