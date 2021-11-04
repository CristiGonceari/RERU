import { TestBed } from '@angular/core/testing';

import { DocumentsTemplateService } from './documents-template.service';

describe('DocumentsTemplateService', () => {
  let service: DocumentsTemplateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DocumentsTemplateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
