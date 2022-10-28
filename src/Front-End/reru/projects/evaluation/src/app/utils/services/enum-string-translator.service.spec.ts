import { TestBed } from '@angular/core/testing';

import { EnumStringTranslatorService } from './enum-string-translator.service';

describe('EnumStringTranslatorService', () => {
  let service: EnumStringTranslatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnumStringTranslatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
