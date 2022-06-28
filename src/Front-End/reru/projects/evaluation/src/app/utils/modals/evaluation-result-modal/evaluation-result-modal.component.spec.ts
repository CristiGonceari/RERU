import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EvaluationResultModalComponent } from './evaluation-result-modal.component';

describe('EvaluationResultModalComponent', () => {
  let component: EvaluationResultModalComponent;
  let fixture: ComponentFixture<EvaluationResultModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EvaluationResultModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EvaluationResultModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
