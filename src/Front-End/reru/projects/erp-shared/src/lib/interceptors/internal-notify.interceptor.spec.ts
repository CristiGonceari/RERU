import { TestBed } from '@angular/core/testing';

import { InternalNotifyInterceptor } from './internal-notify.interceptor';

describe('InternalNotifyInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      InternalNotifyInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: InternalNotifyInterceptor = TestBed.inject(InternalNotifyInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
