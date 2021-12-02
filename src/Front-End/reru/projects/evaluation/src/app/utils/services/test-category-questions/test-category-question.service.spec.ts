import { TestBed } from '@angular/core/testing';

import { TestCategoryQuestionService } from '../test-category-questions/test-category-question.service';

describe('TestCategoryQuestionService', () => {
  let service: TestCategoryQuestionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestCategoryQuestionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
