import { TestBed } from '@angular/core/testing';

import { TestTemplateQuestionCategoryService } from './test-template-question-category.service';

describe('TestTemplateQuestionCategoryService', () => {
  let service: TestTemplateQuestionCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestTemplateQuestionCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
