import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StepperArrowHintComponent } from './stepper-arrow-hint.component';

describe('StepperArrowHintComponent', () => {
  let component: StepperArrowHintComponent;
  let fixture: ComponentFixture<StepperArrowHintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StepperArrowHintComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StepperArrowHintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
