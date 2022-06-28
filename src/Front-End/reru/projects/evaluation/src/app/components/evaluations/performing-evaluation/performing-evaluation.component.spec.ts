import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PerformingEvaluationComponent } from './performing-evaluation.component';

describe('PerformingEvaluationComponent', () => {
  let component: PerformingEvaluationComponent;
  let fixture: ComponentFixture<PerformingEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PerformingEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PerformingEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
