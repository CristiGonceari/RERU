import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeToDateComponent } from './employee-to-date.component';

describe('EmployeeToDateComponent', () => {
  let component: EmployeeToDateComponent;
  let fixture: ComponentFixture<EmployeeToDateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeToDateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeToDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
