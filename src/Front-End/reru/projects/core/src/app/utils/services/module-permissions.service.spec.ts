import { TestBed } from '@angular/core/testing';

import { ModulePermissionsService } from './module-permissions.service';

describe('ModulePermissionsService', () => {
  let service: ModulePermissionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModulePermissionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
