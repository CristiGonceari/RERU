import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewSolicitedVacandPositionModalComponent } from './review-solicited-vacand-position-modal.component';

describe('ReviewSolicitedVacandPositionModalComponent', () => {
  let component: ReviewSolicitedVacandPositionModalComponent;
  let fixture: ComponentFixture<ReviewSolicitedVacandPositionModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReviewSolicitedVacandPositionModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewSolicitedVacandPositionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
