import { TestBed } from '@angular/core/testing';

import { QuestionByCategoryService } from './question-by-category.service';

describe('QuestionByCategoryService', () => {
  let service: QuestionByCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuestionByCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
