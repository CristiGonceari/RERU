import { TestBed } from '@angular/core/testing';

import { ModernLanguageLevelService } from './modern-language-level.service';

describe('ModernLanguageLevelService', () => {
  let service: ModernLanguageLevelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModernLanguageLevelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
