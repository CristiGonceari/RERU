import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StepperArrowHintDownComponent } from './stepper-arrow-hint-down.component';

describe('StepperArrowHintDownComponent', () => {
  let component: StepperArrowHintDownComponent;
  let fixture: ComponentFixture<StepperArrowHintDownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StepperArrowHintDownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StepperArrowHintDownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
