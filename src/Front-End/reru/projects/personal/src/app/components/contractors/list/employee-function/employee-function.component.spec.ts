import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeFunctionComponent } from './employee-function.component';

describe('EmployeeFunctionComponent', () => {
  let component: EmployeeFunctionComponent;
  let fixture: ComponentFixture<EmployeeFunctionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeFunctionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeFunctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
