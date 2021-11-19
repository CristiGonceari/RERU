import { TestBed } from '@angular/core/testing';

import { ModuleRolesService } from './module-roles.service';

describe('ModuleRolesService', () => {
  let service: ModuleRolesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModuleRolesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
