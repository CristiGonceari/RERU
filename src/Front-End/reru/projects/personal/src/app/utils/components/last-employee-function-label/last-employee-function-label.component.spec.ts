import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastEmployeeFunctionLabelComponent } from './last-employee-function-label.component';

describe('LastEmployeeFunctionLabelComponent', () => {
  let component: LastEmployeeFunctionLabelComponent;
  let fixture: ComponentFixture<LastEmployeeFunctionLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LastEmployeeFunctionLabelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LastEmployeeFunctionLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
