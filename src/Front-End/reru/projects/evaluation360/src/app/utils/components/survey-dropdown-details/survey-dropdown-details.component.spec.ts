import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyDropdownDetailsComponent } from './survey-dropdown-details.component';

describe('SurveyDropdownDetailsComponent', () => {
  let component: SurveyDropdownDetailsComponent;
  let fixture: ComponentFixture<SurveyDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
