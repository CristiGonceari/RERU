import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyAcceptComponent } from './survey-accept.component';

describe('SurveyAcceptComponent', () => {
  let component: SurveyAcceptComponent;
  let fixture: ComponentFixture<SurveyAcceptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyAcceptComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyAcceptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
