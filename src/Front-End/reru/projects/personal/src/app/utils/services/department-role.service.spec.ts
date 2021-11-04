import { TestBed } from '@angular/core/testing';

import { DepartmentRoleService } from './department-role.service';

describe('DepartmentRoleService', () => {
  let service: DepartmentRoleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DepartmentRoleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
