import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEvaluationResultComponent } from './view-evaluation-result.component';

describe('ViewEvaluationResultComponent', () => {
  let component: ViewEvaluationResultComponent;
  let fixture: ComponentFixture<ViewEvaluationResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewEvaluationResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewEvaluationResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
