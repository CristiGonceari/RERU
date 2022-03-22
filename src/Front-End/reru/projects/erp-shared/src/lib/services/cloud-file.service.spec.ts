import { TestBed } from '@angular/core/testing';

import { CloudFileService } from './cloud-file.service';

describe('CloudFileService', () => {
  let service: CloudFileService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CloudFileService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
