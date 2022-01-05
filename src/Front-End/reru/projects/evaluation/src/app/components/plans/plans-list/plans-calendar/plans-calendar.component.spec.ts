import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlansCalendarComponent } from './plans-calendar.component';

describe('PlansListTableComponent', () => {
  let component: PlansCalendarComponent;
  let fixture: ComponentFixture<PlansCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlansCalendarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlansCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
