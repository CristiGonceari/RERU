import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateRegistrationFluxComponent } from './candidate-registration-flux.component';

describe('CandidateRegistrationFluxComponent', () => {
  let component: CandidateRegistrationFluxComponent;
  let fixture: ComponentFixture<CandidateRegistrationFluxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CandidateRegistrationFluxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateRegistrationFluxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
