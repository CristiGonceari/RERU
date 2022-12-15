import { TestBed } from '@angular/core/testing';

import { StyleNodesService } from './style-nodes.service';

describe('StyleNodesService', () => {
  let service: StyleNodesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StyleNodesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
