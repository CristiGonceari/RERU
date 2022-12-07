import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyCountersignComponent } from './survey-countersign.component';

describe('SurveyCountersignComponent', () => {
  let component: SurveyCountersignComponent;
  let fixture: ComponentFixture<SurveyCountersignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyCountersignComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyCountersignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
