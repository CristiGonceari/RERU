import { TestBed } from '@angular/core/testing';

import { GetBulkProgressHistoryService } from './get-bulk-progress-history.service';

describe('GetBulkProgressHistoryService', () => {
  let service: GetBulkProgressHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GetBulkProgressHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
