import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StepperDateTimeComponent } from './stepper-date-time.component';

describe('StepperDateTimeComponent', () => {
  let component: StepperDateTimeComponent;
  let fixture: ComponentFixture<StepperDateTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StepperDateTimeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StepperDateTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
