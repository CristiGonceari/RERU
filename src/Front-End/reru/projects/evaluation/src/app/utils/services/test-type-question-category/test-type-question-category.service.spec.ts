import { TestBed } from '@angular/core/testing';

import { TestTypeQuestionCategoryService } from './test-type-question-category.service';

describe('TestTypeQuestionCategoryService', () => {
  let service: TestTypeQuestionCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestTypeQuestionCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
