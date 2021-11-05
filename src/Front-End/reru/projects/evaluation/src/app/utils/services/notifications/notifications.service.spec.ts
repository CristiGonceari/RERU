import { TestBed } from '@angular/core/testing';

import { NotificationsApiService } from './notifications.service';

describe('NotificationsService', () => {
  let service: NotificationsApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NotificationsApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
