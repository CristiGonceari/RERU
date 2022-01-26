import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeFromDateComponent } from './employee-from-date.component';

describe('EmployeeFromDateComponent', () => {
  let component: EmployeeFromDateComponent;
  let fixture: ComponentFixture<EmployeeFromDateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeFromDateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeFromDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
