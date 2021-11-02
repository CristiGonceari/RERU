import { TestBed } from '@angular/core/testing';

import { VacantionService } from './vacantion.service';

describe('VacationService', () => {
  let service: VacantionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VacantionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});