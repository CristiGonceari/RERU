import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditTimesheetValuesComponent } from './add-edit-timesheet-values.component';

describe('AddEditTimesheetValuesComponent', () => {
  let component: AddEditTimesheetValuesComponent;
  let fixture: ComponentFixture<AddEditTimesheetValuesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditTimesheetValuesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEditTimesheetValuesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
