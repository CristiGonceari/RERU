import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacationDropdownDetailsComponent } from './vacation-dropdown-details.component';

describe('VacationDropdownDetailsComponent', () => {
  let component: VacationDropdownDetailsComponent;
  let fixture: ComponentFixture<VacationDropdownDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacationDropdownDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VacationDropdownDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
