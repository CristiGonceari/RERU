import { TestBed } from '@angular/core/testing';

import { FileTestAnswerService } from './file-test-answer.service';

describe('FileTestAnswerService', () => {
  let service: FileTestAnswerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FileTestAnswerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
