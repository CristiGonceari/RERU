import { TestBed } from '@angular/core/testing';

import { IdnpInterceptor } from './idnp.interceptor';

describe('IdnpInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [ IdnpInterceptor ]
  }));

  it('should be created', () => {
    const interceptor: IdnpInterceptor = TestBed.inject(IdnpInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
