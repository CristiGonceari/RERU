import { TestBed } from '@angular/core/testing';

import { TestTemplateService } from './test-template.service';

describe('TestTemplateService', () => {
  let service: TestTemplateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestTemplateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
