import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyEvaluateComponent } from './survey-evaluate.component';

describe('SurveyEvaluateComponent', () => {
  let component: SurveyEvaluateComponent;
  let fixture: ComponentFixture<SurveyEvaluateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyEvaluateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyEvaluateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
