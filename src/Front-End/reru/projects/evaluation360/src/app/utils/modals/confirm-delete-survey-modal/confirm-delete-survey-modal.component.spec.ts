import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmDeleteSurveyModalComponent } from './confirm-delete-survey-modal.component';

describe('ConfirmDeleteSurveyModalComponent', () => {
  let component: ConfirmDeleteSurveyModalComponent;
  let fixture: ComponentFixture<ConfirmDeleteSurveyModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmDeleteSurveyModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmDeleteSurveyModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
