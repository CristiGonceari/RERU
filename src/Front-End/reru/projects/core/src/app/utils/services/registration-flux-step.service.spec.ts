import { TestBed } from '@angular/core/testing';

import { RegistrationFluxStepService } from './registration-flux-step.service';

describe('RegistrationFluxStepService', () => {
  let service: RegistrationFluxStepService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegistrationFluxStepService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
