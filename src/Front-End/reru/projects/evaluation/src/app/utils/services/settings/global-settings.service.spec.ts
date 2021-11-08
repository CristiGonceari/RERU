import { TestBed } from '@angular/core/testing';

import { GlobalSettingsService } from '../settings/global-settings.service';

describe('GlobalSettingsService', () => {
  let service: GlobalSettingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalSettingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
