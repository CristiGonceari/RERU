import { TestBed } from '@angular/core/testing';

import { RecommendationForStudyService } from './recommendation-for-study.service';

describe('RecommendationForStudyService', () => {
  let service: RecommendationForStudyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecommendationForStudyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
