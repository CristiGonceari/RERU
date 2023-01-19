import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeFunctionTableComponent } from './employee-function-table.component';

describe('EmployeeFunctionTableComponent', () => {
  let component: EmployeeFunctionTableComponent;
  let fixture: ComponentFixture<EmployeeFunctionTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeFunctionTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeFunctionTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
