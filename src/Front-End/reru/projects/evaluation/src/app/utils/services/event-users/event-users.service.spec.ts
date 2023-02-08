import { TestBed } from '@angular/core/testing';

import { EventUsersService } from './event-users.service';

describe('EventUsersService', () => {
  let service: EventUsersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventUsersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
