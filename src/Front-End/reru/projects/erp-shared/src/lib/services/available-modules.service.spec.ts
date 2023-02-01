/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { AvailableModuleService } from './available-modules.service';

describe('Service: AvailableModule', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AvailableModuleService]
    });
  });

  it('should ...', inject([AvailableModuleService], (service: AvailableModuleService) => {
    expect(service).toBeTruthy();
  }));
});
