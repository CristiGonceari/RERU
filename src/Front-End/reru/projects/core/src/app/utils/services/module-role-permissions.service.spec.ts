import { TestBed } from '@angular/core/testing';

import { ModuleRolePermissionsService } from './module-role-permissions.service';

describe('ModuleRolePermissionsService', () => {
  let service: ModuleRolePermissionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModuleRolePermissionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
