import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HolidayDropdownDetailsComponent } from './holiday-dropdown-details.component';

describe('HolidayDropdownDetailsComponent', () => {
  let component: HolidayDropdownDetailsComponent;
  let fixture: ComponentFixture<HolidayDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HolidayDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HolidayDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
