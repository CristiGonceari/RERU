import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigureHolidayComponent } from './configure-holiday.component';

describe('ConfigureHolidayComponent', () => {
  let component: ConfigureHolidayComponent;
  let fixture: ComponentFixture<ConfigureHolidayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigureHolidayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigureHolidayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
