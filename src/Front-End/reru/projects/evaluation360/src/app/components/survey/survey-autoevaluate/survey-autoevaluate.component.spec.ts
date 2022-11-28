import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyAutoevaluateComponent } from './survey-autoevaluate.component';

describe('SurveyAutoevaluateComponent', () => {
  let component: SurveyAutoevaluateComponent;
  let fixture: ComponentFixture<SurveyAutoevaluateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyAutoevaluateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyAutoevaluateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
