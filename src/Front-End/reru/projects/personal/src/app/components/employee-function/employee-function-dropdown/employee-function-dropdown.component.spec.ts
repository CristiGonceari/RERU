import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeFunctionDropdownComponent } from './employee-function-dropdown.component';

describe('EmployeeFunctionDropdownComponent', () => {
  let component: EmployeeFunctionDropdownComponent;
  let fixture: ComponentFixture<EmployeeFunctionDropdownComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeFunctionDropdownComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeFunctionDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
