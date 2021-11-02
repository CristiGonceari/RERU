import { TestBed } from '@angular/core/testing';

import { DepartmentContentService } from './department-content.service';

describe('DepartmentContentService', () => {
  let service: DepartmentContentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DepartmentContentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
