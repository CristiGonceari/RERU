import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluationsDropdownDetailsComponent } from './evaluations-dropdown-details.component';

describe('EvaluationsDropdownDetailsComponent', () => {
  let component: EvaluationsDropdownDetailsComponent;
  let fixture: ComponentFixture<EvaluationsDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EvaluationsDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EvaluationsDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
