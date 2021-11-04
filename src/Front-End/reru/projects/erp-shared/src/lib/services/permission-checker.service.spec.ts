/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { PermissionCheckerService } from './permission-checker.service';

describe('Service: PermissionChecker', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PermissionCheckerService]
    });
  });

  it('should ...', inject([PermissionCheckerService], (service: PermissionCheckerService) => {
    expect(service).toBeTruthy();
  }));
});
