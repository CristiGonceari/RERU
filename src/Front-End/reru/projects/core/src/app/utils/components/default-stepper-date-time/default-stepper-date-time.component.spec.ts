import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultStepperDateTimeComponent } from './default-stepper-date-time.component';

describe('DefaultStepperDateTimeComponent', () => {
  let component: DefaultStepperDateTimeComponent;
  let fixture: ComponentFixture<DefaultStepperDateTimeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefaultStepperDateTimeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultStepperDateTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
